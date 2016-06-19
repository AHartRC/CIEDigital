using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CIEDigitalLib.Models.Binding
{
    public class PlayerPosition
    {
        public PlayerPosition()
        {
            Combines = new HashSet<Combine>();
            Players = new HashSet<Player>();
        }

        public int ID { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Combine> Combines { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Player> Players { get; set; }
    }
}