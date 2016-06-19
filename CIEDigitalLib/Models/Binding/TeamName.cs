using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CIEDigitalLib.Models.Binding
{
    public class TeamName
    {
        public int ID { get; set; }
        public int CurrentTeamID { get; set; }
        public int? EndYear { get; set; }
        public int OrganizationID { get; set; }
        public int? PreviousTeamID { get; set; }
        public int StartYear { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Organization Organization { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Team CurrentTeam { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Team PreviousTeam { get; set; }
    }
}