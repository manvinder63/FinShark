using api.Dtos;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
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

        [HttpPost("api/comment")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentdt)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var commentModel = commentdt.ToCommentFromCreateDTO();

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