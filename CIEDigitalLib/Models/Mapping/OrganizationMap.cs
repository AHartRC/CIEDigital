using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class OrganizationMap : EntityTypeConfiguration<Organization>
    {
        public OrganizationMap()
        {
            ToTable("Organization");

            Property(t => t.ID)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasParameterName("ID")
                .IsRequired();

            Property(t => t.FullName)
                .HasColumnName("FullName")
                .HasColumnOrder(2)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(64)
                .HasParameterName("FullName")
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

            HasMany(m => m.TeamNames)
                .WithRequired(r => r.Organization)
                .HasForeignKey(fk => fk.OrganizationID);
        }
    }
}