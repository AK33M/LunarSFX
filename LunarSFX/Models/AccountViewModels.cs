using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LunarSFX.Models
{
    public class LogInViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name ="Username")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
