using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CIEDigitalLib.Models.Binding
{
    public class Organization
    {
        public Organization()
        {
            TeamNames = new HashSet<TeamName>();
        }

        public int ID { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<TeamName> TeamNames { get; set; }
    }
}