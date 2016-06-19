using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using CIEDigitalLib.Models.Binding;
using CIEDigitalLib.Models.Mapping;

namespace CIEDigitalLib.Models.Context
{
    public class CIEDigitalEntities : DbContext
    {
        static CIEDigitalEntities()
        {
            Database.SetInitializer<CIEDigitalEntities>(null);
        }

        public CIEDigitalEntities() : base("name=DefaultConnection")
        {
        }

        public CIEDigitalEntities(string connName) : base(connName)
        {
        }

        public virtual DbSet<Combine> Combines { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Play> Plays { get; set; }
        public virtual DbSet<Player> Players { get; set; }

        [Display(Name = "Player Positions")]
        public virtual DbSet<PlayerPosition> PlayerPositions { get; set; }

        [Display(Name = "Game Results")]
        public virtual DbSet<GameResult> GameResults { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        [Display(Name = "Team Names")]
        public virtual DbSet<TeamName> TeamNames { get; set; }

        [Display(Name = "Weather Conditions")]
        public virtual DbSet<Weather> Weathers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CombineMap());
            modelBuilder.Configurations.Add(new OrganizationMap());
            modelBuilder.Configurations.Add(new PlayMap());
            modelBuilder.Configurations.Add(new PlayerMap());
            modelBuilder.Configurations.Add(new PlayerPositionMap());
            modelBuilder.Configurations.Add(new GameResultMap());
            modelBuilder.Configurations.Add(new TeamMap());
            modelBuilder.Configurations.Add(new TeamNameMap());
            modelBuilder.Configurations.Add(new WeatherMap());
        }
    }
}