namespace EpiChatApp.Models
{
	public class Chat
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string ImagePath { get; set; } = null!;
		public ChatType Type { get; set; }
		public ICollection<Message> Messages { get; set; }
		public ICollection<ChatUser> ChatUsers { get; set; }
		public Chat()
		{
			Messages = new List<Message>();
			ChatUsers = new List<ChatUser>();
		}
	}
}
