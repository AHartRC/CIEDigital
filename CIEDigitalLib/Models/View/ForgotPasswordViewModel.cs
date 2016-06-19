using System.ComponentModel.DataAnnotations;

namespace CIEDigitalLib.Models.View
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}