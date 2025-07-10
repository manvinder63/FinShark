using api.Dtos;
using api.Dtos.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
         private readonly IFMPService _fmpService;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepository, UserManager<AppUser> userManager,IFMPService fMPService)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepository;
            _userManager = userManager;
             _fmpService = fMPService;
        }

        [HttpGet]
        [Route("api/comment")]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("api/comment/{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
         [Route("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentRequestDto commentdt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);

                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepo.CreateAsync(stock);
                }
            }


            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentdt.ToCommentFromCreateDTO(stock.Id);
            commentModel.AppUserId = appUser.Id;

            var createdComment = await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = createdComment?.Id }, createdComment?.ToCommentDto());
        }

        [HttpPut("api/comment/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var commentModel = await _commentRepo.UpdateAsync(id, updateDto);

            if (commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete("api/comment/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var deletedComment = await _commentRepo.DeleteAsync(id);

            if (deletedComment == null)
            {
                return NotFound();
            }

            return Ok(deletedComment.ToCommentDto());
        }
    }
}