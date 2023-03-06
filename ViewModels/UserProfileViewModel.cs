using System.ComponentModel.DataAnnotations;

namespace EpiChatApp.ViewModels
{
	public class UserProfileViewModel
	{
		public string Name { get; set; } = null!;
		[EmailAddress]
		public string Email { get; set; } = null!;
	}
}
