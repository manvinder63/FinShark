using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo,
            IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            Console.WriteLine($"Username: {username}");
            var appUser = await _userManager.FindByNameAsync(username);
            Console.WriteLine($"AppUser: {appUser?.UserName}");
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            Console.WriteLine($"User Portfolio Count: {userPortfolio}");
            return Ok(userPortfolio);

        }
    }
}