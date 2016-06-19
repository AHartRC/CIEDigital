namespace CIEDigitalLib.Models.Binding
{
    public class Combine
    {
        public int ID { get; set; }
        public decimal Arms { get; set; }
        public decimal Bench { get; set; }
        public decimal Broad { get; set; }
        public string College { get; set; }
        public string FirstName { get; set; }
        public decimal FourtyYardDash { get; set; }
        public decimal Hands { get; set; }
        public decimal HeightFeet { get; set; }
        public decimal HeightInches { get; set; }
        public decimal HeightInchesTotal { get; set; }
        public string Name { get; set; }
        public decimal NFLGrade { get; set; }
        public string LastName { get; set; }
        public string Pick { get; set; }
        public int PickRound { get; set; }
        public int PickTotal { get; set; }
        public int PositionID { get; set; }
        public decimal Round { get; set; }
        public decimal TenYardDash { get; set; }
        public decimal ThreeCone { get; set; }
        public decimal TwentyYardDash { get; set; }
        public decimal TwentyYardShuttle { get; set; }
        public decimal Vertical { get; set; }
        public decimal WeightPounds { get; set; }
        public int Wonderlic { get; set; }
        public int Year { get; set; }

        public virtual PlayerPosition Position { get; set; }
    }
}