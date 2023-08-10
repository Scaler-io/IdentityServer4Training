using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Quickstart.Account
{
    public class RegistrationViewModel
    {
        //[Required(ErrorMessage = "Firstname is required")]
        //public string FirstName { get; set; }
        //[Required(ErrorMessage = "Firstname is required")]
        //public string LastName { get; set; }
        //[Required(ErrorMessage = "Address is required")]
        //public string Address { get; set; }
        //[Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did not match")]
        public string ConfirmPassword { get; set; }
    }
}