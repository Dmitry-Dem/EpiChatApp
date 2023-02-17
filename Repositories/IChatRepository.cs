using EpiChatApp.Models;
using EpiChatApp.ViewModels;

namespace EpiChatApp.Repositories
{
    public interface IChatRepository
    {
        Task<int> CreateChat(ChatViewModel chatViewModel, string userId);
		Task CreateMessage(Message newMessage);
		Chat GetChat(int id);
		Task<bool> IsUserSuccessfullyJoined(Chat chat, string userId);
        IEnumerable<Chat> GetChatsByName(string name);
        bool IsUserJoined(Chat chat, string userId);
	}
}
