using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CIEDigitalLib.Models.Binding
{
    public class Team
    {
        public Team()
        {
            OffensivePlays = new HashSet<Play>();
            DefensivePlays = new HashSet<Play>();
            Players = new HashSet<Player>();
            CurrentTeamNames = new HashSet<TeamName>();
            PreviousTeamNames = new HashSet<TeamName>();
            HomeWeather = new HashSet<Weather>();
            AwayWeather = new HashSet<Weather>();
            HomeGames = new HashSet<GameResult>();
            AwayGames = new HashSet<GameResult>();
        }

        public int ID { get; set; }
        public string Franchise { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Play> OffensivePlays { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Play> DefensivePlays { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Player> Players { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<TeamName> CurrentTeamNames { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<TeamName> PreviousTeamNames { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Weather> HomeWeather { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Weather> AwayWeather { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<GameResult> HomeGames { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<GameResult> AwayGames { get; set; }
    }
}