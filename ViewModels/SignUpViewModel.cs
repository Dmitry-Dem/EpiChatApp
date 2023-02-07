using System.ComponentModel.DataAnnotations;

namespace EpiChatApp.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; } = null!;
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; } = null!;
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
		public string PasswordConfirm { get; set; } = null!;
	}
}
