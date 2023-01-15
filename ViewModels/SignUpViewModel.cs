using System.ComponentModel.DataAnnotations;

namespace EpiChatApp.ViewModels
{
    public class SignUpViewModel : SignInViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
