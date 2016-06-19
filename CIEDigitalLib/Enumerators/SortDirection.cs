using System.Runtime.Serialization;

namespace CIEDigitalLib.Enumerators
{
    [DataContract]
    public enum SortDirection
    {
        [EnumMember] Ascending,

        [EnumMember] Descending
    }
}