using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class TeamMap : EntityTypeConfiguration<Team>
    {
        public TeamMap()
        {
            ToTable("Team");

            Property(t => t.ID)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasParameterName("ID")
                .IsRequired();

            Property(t => t.Franchise)
                .HasColumnName("Franchise")
                .HasColumnOrder(2)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("Franchise")
                .IsRequired();

            Property(t => t.Location)
                .HasColumnName("Location")
                .HasColumnOrder(3)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("Location")
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnOrder(4)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(64)
                .HasParameterName("Name")
                .IsRequired();

            Property(t => t.ShortName)
                .HasColumnName("ShortName")
                .HasColumnOrder(5)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(5)
                .HasParameterName("ShortName")
                .IsRequired();

            HasKey(t => t.ID);

            HasMany(m => m.OffensivePlays)
                .WithOptional(r => r.Offense)
                .HasForeignKey(fk => fk.OffenseID);

            HasMany(m => m.DefensivePlays)
                .WithOptional(r => r.Defense)
                .HasForeignKey(fk => fk.DefenseID);

            HasMany(m => m.Players)
                .WithOptional(r => r.CurrentTeam)
                .HasForeignKey(fk => fk.CurrentTeamID);

            HasMany(m => m.CurrentTeamNames)
                .WithRequired(r => r.CurrentTeam)
                .HasForeignKey(fk => fk.CurrentTeamID);

            HasMany(m => m.PreviousTeamNames)
                .WithOptional(r => r.PreviousTeam)
                .HasForeignKey(fk => fk.PreviousTeamID);

            HasMany(m => m.HomeWeather)
                .WithRequired(r => r.HomeTeam)
                .HasForeignKey(fk => fk.HomeTeamID)
                .WillCascadeOnDelete(false);

            HasMany(m => m.AwayWeather)
                .WithRequired(r => r.AwayTeam)
                .HasForeignKey(fk => fk.AwayTeamID)
                .WillCascadeOnDelete(false);

            HasMany(m => m.AwayGames)
                .WithRequired(r => r.AwayTeam)
                .HasForeignKey(fk => fk.AwayTeamID)
                .WillCascadeOnDelete(false);

            HasMany(m => m.HomeGames)
                .WithRequired(r => r.HomeTeam)
                .HasForeignKey(fk => fk.HomeTeamID)
                .WillCascadeOnDelete(false);
        }
    }
}