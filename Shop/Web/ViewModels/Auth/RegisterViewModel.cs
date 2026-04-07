using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage= "Full name is required")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; } = string.Empty;

        [Required(ErrorMessage= "Email is required")]
        [EmailAddress(ErrorMessage = " Invalid email address")]
        [Display(Name = " Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be atleast 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage =" Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage =" Password do not match")]
        [Display(Name ="Confirm password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Phone(ErrorMessage ="Invalid phone number")]
        [Display(Name ="Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
