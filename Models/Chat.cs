namespace EpiChatApp.Models
{
	public class Chat
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string ImagePath { get; set; } = null!;
		public ChatType Type { get; set; }
		public ICollection<Messege> Messeges { get; set; }
		public ICollection<ChatUser> ChatUsers { get; set; }
		public Chat()
		{
			Messeges = new List<Messege>();
			ChatUsers = new List<ChatUser>();
		}
	}
}
