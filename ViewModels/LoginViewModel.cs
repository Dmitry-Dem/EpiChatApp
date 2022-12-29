using System.ComponentModel.DataAnnotations;

namespace EpiChatApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Addres")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
