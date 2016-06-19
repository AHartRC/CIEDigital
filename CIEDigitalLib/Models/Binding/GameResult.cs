using System;

namespace CIEDigitalLib.Models.Binding
{
    public class GameResult
    {
        public int ID { get; set; }
        public int AwayScore { get; set; }
        public int AwayTeamID { get; set; }
        public int HomeTeamID { get; set; }
        public int HomeScore { get; set; }
        public DateTime KickOff { get; set; }
        public int Season { get; set; }
        public int Week { get; set; }

        public virtual Team AwayTeam { get; set; }
        public virtual Team HomeTeam { get; set; }
    }
}