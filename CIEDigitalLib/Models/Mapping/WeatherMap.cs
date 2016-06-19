using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class WeatherMap : EntityTypeConfiguration<Weather>
    {
        public WeatherMap()
        {
            ToTable("Weather");

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

            Property(t => t.Date)
                .HasColumnName("Date")
                .HasColumnOrder(4)
                .HasColumnType("DATE")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Date")
                .IsRequired();

            Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnOrder(5)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(128)
                .HasParameterName("Description")
                .IsRequired();

            Property(t => t.GameID)
                .HasColumnName("GameID")
                .HasColumnOrder(6)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("GameID")
                .IsRequired();

            Property(t => t.Humidity)
                .HasColumnName("Humidity")
                .HasColumnOrder(7)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Humidity")
                .IsOptional();

            Property(t => t.HomeScore)
                .HasColumnName("HomeScore")
                .HasColumnOrder(8)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("HomeScore")
                .IsRequired();

            Property(t => t.HomeTeamID)
                .HasColumnName("HomeTeamID")
                .HasColumnOrder(9)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("HomeTeamID")
                .IsRequired();

            Property(t => t.Temperature)
                .HasColumnName("Temperature")
                .HasColumnOrder(10)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Temperature")
                .IsRequired();

            Property(t => t.WindChill)
                .HasColumnName("WindChill")
                .HasColumnOrder(11)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("WindChill")
                .IsOptional();

            Property(t => t.WindMPH)
                .HasColumnName("WindMPH")
                .HasColumnOrder(12)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("WindMPH")
                .IsOptional();

            HasKey(t => t.ID);

            HasRequired(r => r.AwayTeam)
                .WithMany(m => m.AwayWeather)
                .HasForeignKey(fk => fk.AwayTeamID);

            HasRequired(r => r.HomeTeam)
                .WithMany(m => m.HomeWeather)
                .HasForeignKey(fk => fk.HomeTeamID);
        }
    }
}