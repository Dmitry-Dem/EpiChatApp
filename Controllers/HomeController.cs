using EpiChatApp.Models;
using EpiChatApp.Repositories;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Claims;

namespace EpiChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IChatRepository _chatRepository;
        public HomeController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
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
                var chatId = await _chatRepository.CreateChat(chatViewModel, GetUserId());

                return RedirectToAction("Chat", new { id = chatId });
            }

			return View();
		}
        [Route("Home/Chat/{id}")]
        public IActionResult Chat(int id)
		{
			return View(_chatRepository.GetChat(id));
		}
        [HttpPost]
		public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            if (message.IsNullOrEmpty() != true)
            {
                var newMessage = new Message
                {
                    ChatId = chatId,
                    Text = message,
                    Name = User.Identity.Name,
                    Timestamp = DateTime.Now
                };

                await _chatRepository.CreateMessage(newMessage);
            }

            return RedirectToAction("Chat", new { id = chatId });
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