using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CIEDigitalLib.Models.Binding
{
    public class Player
    {
        public int ID { get; set; }
        public int NFLID { get; set; }
        public string Slug { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string RawStats { get; set; }
        public int? CurrentTeamID { get; set; }
        public int? PositionID { get; set; }
        public int? Number { get; set; }
        public string Status { get; set; }
        public int? Years { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? HallOfFameYear { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual PlayerPosition PlayerPosition { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Team CurrentTeam { get; set; }
    }
}