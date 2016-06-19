using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class PlayMap : EntityTypeConfiguration<Play>
    {
        public PlayMap()
        {
            ToTable("Play");

            Property(t => t.ID)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasParameterName("ID")
                .IsRequired();

            Property(t => t.DefenseID)
                .HasColumnName("DefenseID")
                .HasColumnOrder(2)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("DefenseID")
                .IsOptional();

            Property(t => t.DefenseScore)
                .HasColumnName("DefenseScore")
                .HasColumnOrder(3)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("DefenseScore")
                .IsOptional();

            Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnOrder(4)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(1024)
                .HasParameterName("Description")
                .IsRequired();

            Property(t => t.Down)
                .HasColumnName("Down")
                .HasColumnOrder(5)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Down")
                .IsRequired();

            Property(t => t.GameDate)
                .HasColumnName("GameDate")
                .HasColumnOrder(6)
                .HasColumnType("DATE")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("GameDate")
                .IsRequired();

            Property(t => t.GameID)
                .HasColumnName("GameID")
                .HasColumnOrder(7)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("GameID")
                .IsRequired();

            Property(t => t.IsBad)
                .HasColumnName("IsBad")
                .HasColumnOrder(8)
                .HasColumnType("BIT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("IsBad")
                .IsRequired();

            Property(t => t.Minute)
                .HasColumnName("Minute")
                .HasColumnOrder(9)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Minute")
                .IsRequired();

            Property(t => t.OffenseID)
                .HasColumnName("OffenseID")
                .HasColumnOrder(10)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("OffenseID")
                .IsOptional();

            Property(t => t.OffenseScore)
                .HasColumnName("OffenseScore")
                .HasColumnOrder(11)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("OffenseScore")
                .IsOptional();

            Property(t => t.Quarter)
                .HasColumnName("Quarter")
                .HasColumnOrder(12)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Quarter")
                .IsRequired();

            Property(t => t.Season)
                .HasColumnName("Season")
                .HasColumnOrder(13)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Season")
                .IsRequired();

            Property(t => t.Second)
                .HasColumnName("Second")
                .HasColumnOrder(14)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Second")
                .IsOptional();

            Property(t => t.ToGo)
                .HasColumnName("ToGo")
                .HasColumnOrder(15)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("ToGo")
                .IsRequired();

            Property(t => t.Yardline)
                .HasColumnName("Yardline")
                .HasColumnOrder(16)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Yardline")
                .IsRequired();

            HasKey(t => t.ID);

            HasOptional(o => o.Defense)
                .WithMany(m => m.DefensivePlays)
                .HasForeignKey(fk => fk.DefenseID);

            HasOptional(o => o.Offense)
                .WithMany(m => m.OffensivePlays)
                .HasForeignKey(fk => fk.OffenseID);
        }
    }
}