using System.ComponentModel.DataAnnotations;


namespace vm_rental.ViewModels.Sign
{
    public class SignInViewModel
    {
        [Required]
        [Display(Name="Username/Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
