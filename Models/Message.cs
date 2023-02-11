namespace EpiChatApp.Models
{
	public class Message
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Text { get; set; } = null!;
		public DateTime Timestamp { get; set; }
		public int ChatId { get; set; }
		public Chat Chat { get; set; } = null!;
	}
}
