using System.ComponentModel.DataAnnotations;

namespace EpiChatApp.ViewModels
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}
