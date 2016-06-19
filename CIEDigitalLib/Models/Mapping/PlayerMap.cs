using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class PlayerMap : EntityTypeConfiguration<Player>
    {
        public PlayerMap()
        {
            ToTable("Player");

            Property(t => t.ID)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasParameterName("ID")
                .IsRequired();

            Property(t => t.CurrentTeamID)
                .HasColumnName("CurrentTeamID")
                .HasColumnOrder(2)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("CurrentTeamID")
                .IsOptional();

            Property(t => t.EndYear)
                .HasColumnName("EndYear")
                .HasColumnOrder(3)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("EndYear")
                .IsOptional();

            Property(t => t.FirstName)
                .HasColumnName("FirstName")
                .HasColumnOrder(4)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("FirstName")
                .IsRequired();

            Property(t => t.FullName)
                .HasColumnName("FullName")
                .HasColumnOrder(5)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("FullName")
                .IsRequired();

            Property(t => t.HallOfFameYear)
                .HasColumnName("HallOfFameYear")
                .HasColumnOrder(6)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("HallOfFameYear")
                .IsOptional();

            Property(t => t.LastName)
                .HasColumnName("LastName")
                .HasColumnOrder(7)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("LastName")
                .IsRequired();

            Property(t => t.NFLID)
                .HasColumnName("NFLID")
                .HasColumnOrder(8)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("NFLID")
                .IsRequired();

            Property(t => t.Number)
                .HasColumnName("Number")
                .HasColumnOrder(9)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Number")
                .IsOptional();

            Property(t => t.PositionID)
                .HasColumnName("PositionID")
                .HasColumnOrder(10)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("PositionID")
                .IsOptional();

            Property(t => t.RawStats)
                .HasColumnName("RawStats")
                .HasColumnOrder(11)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(512)
                .HasParameterName("RawStats")
                .IsRequired();

            Property(t => t.Slug)
                .HasColumnName("Slug")
                .HasColumnOrder(12)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("Slug")
                .IsRequired();

            Property(t => t.StartYear)
                .HasColumnName("StartYear")
                .HasColumnOrder(13)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("StartYear")
                .IsOptional();

            Property(t => t.Status)
                .HasColumnName("Status")
                .HasColumnOrder(14)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(4)
                .HasParameterName("Status")
                .IsOptional();

            Property(t => t.Years)
                .HasColumnName("Years")
                .HasColumnOrder(15)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Years")
                .IsOptional();

            HasKey(t => t.ID);

            HasOptional(o => o.PlayerPosition)
                .WithMany(m => m.Players)
                .HasForeignKey(fk => fk.PositionID);

            HasOptional(o => o.CurrentTeam)
                .WithMany(m => m.Players)
                .HasForeignKey(fk => fk.CurrentTeamID);
        }
    }
}