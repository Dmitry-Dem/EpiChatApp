using EpiChatApp.Models;
using EpiChatApp.ViewModels;

namespace EpiChatApp.Repositories
{
    public interface IChatRepository
    {
        Task<int> CreateChat(ChatViewModel chatViewModel, string userId);
		Task CreateMessage(Message newMessage);
		Chat GetChat(int id);
    }
}
