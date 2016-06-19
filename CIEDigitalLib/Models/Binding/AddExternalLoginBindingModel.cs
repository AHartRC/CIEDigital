using System.ComponentModel.DataAnnotations;

namespace CIEDigitalLib.Models.Binding
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}