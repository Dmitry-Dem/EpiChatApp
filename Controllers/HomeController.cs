using EpiChatApp.Models;
using EpiChatApp.Repositories;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
		public IActionResult GetChatsByName(string chatName)
		{
            if (chatName.IsNullOrEmpty() != true)
            {
                List<Chat> chats = _chatRepository.GetChatsByName(chatName).ToList();

				var json = JsonConvert.SerializeObject(chats);

				return Json(json);
			}
            else
            {
                return StatusCode(400);
            }
		}

		[Route("Home/Chat/{id}")]
		public IActionResult Chat(int id)
		{
			return View(_chatRepository.GetChat(id));
		}
		public IActionResult CreateChat()
		{
			return View();
		}
		[Route("Home/JoinChat/{chatId}")]
		public async Task<IActionResult> JoinChat(int chatId)
        {
            var chat = _chatRepository.GetChat(chatId);

            bool isUserSuccessfullyJoined = await _chatRepository.IsUserSuccessfullyJoined(chat, GetUserId());

			if (isUserSuccessfullyJoined)
            {
				return RedirectToAction("Chat", new { id = chatId });
			}

            return RedirectToAction("Index");
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

				return Ok();
			}
            return StatusCode(400); //bad request
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