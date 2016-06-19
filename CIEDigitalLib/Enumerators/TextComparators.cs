using System.ComponentModel.DataAnnotations;

namespace CIEDigitalLib.Enumerators
{
    public enum TextComparators
    {
        [Display(Name = "Contains")]
        Contains,
        [Display(Name = "==")]
        Equals,
        [Display(Name = "Starts With")]
        StartsWith,
        [Display(Name = "EndsWith")]
        EndsWith
    }
}