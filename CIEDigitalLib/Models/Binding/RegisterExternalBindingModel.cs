using System.ComponentModel.DataAnnotations;

namespace CIEDigitalLib.Models.Binding
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}