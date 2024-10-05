using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; } // 


        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password does not match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
        public string Email { get; set; }

    }
}
