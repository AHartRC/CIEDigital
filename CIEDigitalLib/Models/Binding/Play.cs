using System;

namespace CIEDigitalLib.Models.Binding
{
    public class Play
    {
        public int ID { get; set; }
        public int? DefenseID { get; set; }
        public int? DefenseScore { get; set; }
        public string Description { get; set; }
        public int Down { get; set; }
        public DateTime GameDate { get; set; }
        public string GameID { get; set; }
        public bool IsBad { get; set; }
        public decimal Minute { get; set; }
        public int? OffenseID { get; set; }
        public int? OffenseScore { get; set; }
        public int Quarter { get; set; }
        public int Season { get; set; }
        public decimal? Second { get; set; }
        public decimal ToGo { get; set; }
        public decimal Yardline { get; set; }

        public virtual Team Defense { get; set; }
        public virtual Team Offense { get; set; }
    }
}