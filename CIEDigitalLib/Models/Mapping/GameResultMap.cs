using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class GameResultMap : EntityTypeConfiguration<GameResult>
    {
        public GameResultMap()
        {
            ToTable("GameResult");

            Property(t => t.ID)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasParameterName("ID")
                .IsRequired();

            Property(t => t.AwayScore)
                .HasColumnName("AwayScore")
                .HasColumnOrder(2)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("AwayScore")
                .IsRequired();

            Property(t => t.AwayTeamID)
                .HasColumnName("AwayTeamID")
                .HasColumnOrder(3)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("AwayTeamID")
                .IsRequired();

            Property(t => t.HomeScore)
                .HasColumnName("HomeScore")
                .HasColumnOrder(4)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("HomeScore")
                .IsRequired();

            Property(t => t.HomeTeamID)
                .HasColumnName("HomeTeamID")
                .HasColumnOrder(5)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("HomeTeamID")
                .IsRequired();

            Property(t => t.KickOff)
                .HasColumnName("KickOff")
                .HasColumnOrder(6)
                .HasColumnType("DATETIME")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("KickOff")
                .IsRequired();

            Property(t => t.Season)
                .HasColumnName("Season")
                .HasColumnOrder(7)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Season")
                .IsRequired();

            Property(t => t.Week)
                .HasColumnName("Week")
                .HasColumnOrder(8)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Week")
                .IsRequired();

            HasKey(t => t.ID);

            HasRequired(r => r.AwayTeam)
                .WithMany(m => m.AwayGames)
                .HasForeignKey(fk => fk.AwayTeamID);

            HasRequired(r => r.HomeTeam)
                .WithMany(m => m.HomeGames)
                .HasForeignKey(fk => fk.HomeTeamID);
        }
    }
}