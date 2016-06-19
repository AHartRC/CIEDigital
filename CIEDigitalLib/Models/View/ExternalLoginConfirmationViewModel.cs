using System.ComponentModel.DataAnnotations;

namespace CIEDigitalLib.Models.View
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}