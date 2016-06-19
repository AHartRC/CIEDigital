using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class PlayerPositionMap : EntityTypeConfiguration<PlayerPosition>
    {
        public PlayerPositionMap()
        {
            ToTable("PlayerPosition");

            Property(t => t.ID)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasParameterName("ID")
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnOrder(2)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("Name")
                .IsRequired();

            Property(t => t.ShortName)
                .HasColumnName("ShortName")
                .HasColumnOrder(3)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(6)
                .HasParameterName("ShortName")
                .IsRequired();

            HasKey(t => t.ID);

            HasMany(m => m.Players)
                .WithOptional(o => o.PlayerPosition)
                .HasForeignKey(fk => fk.PositionID);
        }
    }
}