using System.ComponentModel.DataAnnotations;

namespace CIEDigitalLib.Models.View
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}