using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Username or Email")]
        [Required(ErrorMessage = "Username or email is required")]
        public required string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
