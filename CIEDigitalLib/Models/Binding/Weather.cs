using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CIEDigitalLib.Models.Binding
{
    public class Weather
    {
        public int ID { get; set; }
        public int AwayScore { get; set; }
        public int AwayTeamID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string GameID { get; set; }
        public decimal? Humidity { get; set; }
        public int HomeScore { get; set; }
        public int HomeTeamID { get; set; }
        public decimal Temperature { get; set; }
        public decimal? WindChill { get; set; }
        public decimal? WindMPH { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Team HomeTeam { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Team AwayTeam { get; set; }
    }
}