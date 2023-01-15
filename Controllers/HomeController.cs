using EpiChatApp.Models;
using EpiChatApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EpiChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _context;

		private readonly ILogger<HomeController> _logger;
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userList = await _context.Users.ToListAsync();

			return View(userList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}