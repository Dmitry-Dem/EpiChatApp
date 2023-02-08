namespace EpiChatApp.Models
{
	public class ChatUser
	{
		public string AppUserId { get; set; } = null!;
		public AppUser AppUser { get; set; } = null!;
		public int ChatId { get; set; }
		public Chat Chat { get; set; } = null!;
		public UserRole Role { get; set; }
	}
}
