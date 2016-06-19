using System.ComponentModel.DataAnnotations;

namespace CIEDigitalLib.Enumerators
{
    public enum EqualityComparators
    {
        [Display(Name = "x == y")]
        Equal,
        [Display(Name = "x != y")]
        NotEqual,
    }
}
