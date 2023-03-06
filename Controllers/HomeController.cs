using EpiChatApp.Hubs;
using EpiChatApp.Models;
using EpiChatApp.Repositories;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace EpiChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        public HomeController(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
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
		public async Task<IActionResult> CreateMessage(int chatId, string message, [FromServices] IHubContext<ChatHub> chat)
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

				await chat.Clients.Group(chatId.ToString())
				.SendAsync("RecieveMessage", new
				{
					Text = newMessage.Text,
					Name = newMessage.Name,
					Timestamp = newMessage.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
				});

				return Ok();
			}
            return StatusCode(400); //bad request
        }
        public IActionResult Profile()
        {
            var user = _userRepository.GetUser(GetUserId());

            if (user != null)
            {
				var upDateUser = new UserProfileViewModel()
				{
                    Email = user.Email,
                    Name = user.UserName
				};

                View(upDateUser);
			}

			return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserProfileViewModel upDateUser)
        {
			if (ModelState.IsValid)
            {
                var user = _userRepository.GetUser(GetUserId());

                user.UserName = (upDateUser.Name != "" ? upDateUser.Name : user.UserName);

                if (user.Email != upDateUser.Email)
                {
					//check if email is already in use
					var isAddresAlreadyInUse = await _userRepository.FindUserByEmailAsync(upDateUser.Email) != null;

					if (isAddresAlreadyInUse)
					{
						TempData["Error"] = "Addres is already in use!";
						return RedirectToAction("Profile");
					}

					user.Email = upDateUser.Email;
				}

                var result = await _userRepository.UpdateUserAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "User can't be updated";
            }

            return RedirectToAction("Profile");
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