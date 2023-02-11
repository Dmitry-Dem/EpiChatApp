using EpiChatApp.Data;
using EpiChatApp.Models;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

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
        public async Task<int> CreateChat(ChatViewModel chatViewModel, string userId)
        {
            string chatImagePath = "";

            if (chatViewModel.ChatImage == null)
            {
                chatImagePath = "images\\chat-images\\chat-default-images\\1.jpg";
            }
            else
            {
                string folderPath = $"user-data\\user-id-{userId}\\user-images\\" +
                    $"{Guid.NewGuid().ToString()}_{chatViewModel.ChatImage.FileName}";

                chatImagePath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

                await chatViewModel.ChatImage.CopyToAsync(new FileStream(chatImagePath, FileMode.Create));
            }
            
            var chat = new Chat
            {
                Name = chatViewModel.Name,
                Type = chatViewModel.Type,
                ImagePath = chatImagePath
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
        public Chat GetChat(int id)
        {
            return _applicationDBContext.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(c => c.Id == id);
        }
        public async Task CreateMessage(Message message)
        {
            _applicationDBContext.Messages.Add(message);

            await _applicationDBContext.SaveChangesAsync();
        }
    }
}
