using EpiChatApp.Data;
using EpiChatApp.Models;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EpiChatApp.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private ApplicationDBContext _applicationDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ChatRepository(ApplicationDBContext applicationDBContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDBContext = applicationDBContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<bool> IsUserSuccessfullyJoined(Chat chat, string userId)
        {
			bool isChatPublic = chat.Type == ChatType.Public;
            bool isUserJoined = IsUserJoined(chat, userId);

            if (isUserJoined)
            {
                return true;
            }
            else
            {
                if (isChatPublic)
                {
					var chatUser = new ChatUser
					{
						ChatId = chat.Id,
						AppUserId = userId,
						Role = UserRole.Member
					};

					_applicationDBContext.ChatUsers.Add(chatUser);

					await _applicationDBContext.SaveChangesAsync();

					return true;
				}
            }

            return false;
        }
        public bool IsUserJoined(Chat chat, string userId) => chat.ChatUsers.FirstOrDefault(user => user.AppUserId == userId) != null;
		public Chat? GetChat(int id)
        {
            return _applicationDBContext.Chats
                .Include(x => x.Messages)
                .Include(x => x.ChatUsers)
                .FirstOrDefault(c => c.Id == id);
        }
        public IEnumerable<Chat> GetChatsByName(string name)
        {
            return _applicationDBContext.Chats
                .Where(ch => ch.Name.ToLower() == name.ToLower());
        }
		public async Task<int> CreateChat(ChatViewModel chatViewModel, string userId)
        {
            UserImage userImage = new UserImage(
                userId: userId,
                isNullImage: chatViewModel.ChatImage == null,
                imageName: chatViewModel.ChatImage?.FileName 
                );

            string chatImagePath = getChatImagePath(userImage);

            if (chatViewModel.ChatImage != null)
            {
                string userDataFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, chatImagePath);
                await chatViewModel.ChatImage.CopyToAsync(new FileStream(userDataFolderPath, FileMode.Create));
            }

            var chat = new Chat
            {
                Name = chatViewModel.Name,
                Type = chatViewModel.Type,
                ImagePath = "\\" + chatImagePath
            };

            chat.ChatUsers.Add(new ChatUser
            {
                AppUserId = userId,
                Role = UserRole.Admin
            });

            _applicationDBContext.Chats.Add(chat);

            await _applicationDBContext.SaveChangesAsync();

            return chat.Id;
        }
        public async Task CreateMessage(Message message)
        {
            _applicationDBContext.Messages.Add(message);

            await _applicationDBContext.SaveChangesAsync();
        }
        private string getChatImagePath(UserImage userImage)
        {
            string defaultChatImagePath = "images\\chat-images\\chat-default-images\\1.jpg";
            string chatImagePath;

            if (userImage.isNullImage)
            {
                chatImagePath = defaultChatImagePath;
            }
            else
            {
                chatImagePath = $"user-data\\user-id-{userImage.userId}\\user-images\\{Guid.NewGuid().ToString()}_{userImage.imageName}";
            }

            return chatImagePath;
        }
    }
}
