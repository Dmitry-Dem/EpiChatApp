using EpiChatApp.Models;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EpiChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult CreateChat()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateChat(ChatViewModel chatViewModel)
		{
            if (ModelState.IsValid)
            {
                if (chatViewModel.ChatImage != null)
                {
					string folderPath = $"user-data\\user-id-{GetUserId()}\\user-images\\{Guid.NewGuid().ToString()}_{chatViewModel.ChatImage.FileName}";
                    string fullFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

                    await chatViewModel.ChatImage.CopyToAsync(new FileStream(fullFolderPath, FileMode.Create));
                }

                //Create and save new chat to DB

                return RedirectToAction("Chat");
            }

			return View();
		}
		public IActionResult Chat()
		{
			return View();
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

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}