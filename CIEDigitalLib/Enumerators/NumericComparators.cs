using System.ComponentModel.DataAnnotations;

namespace CIEDigitalLib.Enumerators
{
    public enum NumericComparators
    {
        [Display(Name = "x < y")]
        Less,
        [Display(Name = "x <= y")]
        LessOrEqual,
        [Display(Name = "x == y")]
        Equal,
        [Display(Name = "x != y")]
        NotEqual,
        [Display(Name = "x >= y")]
        GreaterOrEqual,
        [Display(Name = "x > y")]
        Greater,
        [Display(Name = "y >= x =< z")]
        Within,
        [Display(Name = "y > x < z")]
        InRange
    }
}