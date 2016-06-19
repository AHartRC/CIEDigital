using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class TeamNameMap : EntityTypeConfiguration<TeamName>
    {
        public TeamNameMap()
        {
            ToTable("TeamName");

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
                .IsRequired();

            Property(t => t.EndYear)
                .HasColumnName("EndYear")
                .HasColumnOrder(3)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("EndYear")
                .IsOptional();

            Property(t => t.OrganizationID)
                .HasColumnName("OrganizationID")
                .HasColumnOrder(4)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("OrganizationID")
                .IsRequired();

            Property(t => t.PreviousTeamID)
                .HasColumnName("PreviousTeamID")
                .HasColumnOrder(5)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("PreviousTeamID")
                .IsOptional();

            Property(t => t.StartYear)
                .HasColumnName("StartYear")
                .HasColumnOrder(6)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("StartYear")
                .IsRequired();

            HasKey(t => t.ID);

            HasRequired(r => r.CurrentTeam)
                .WithMany(m => m.CurrentTeamNames)
                .HasForeignKey(fk => fk.CurrentTeamID);

            HasRequired(r => r.Organization)
                .WithMany(m => m.TeamNames)
                .HasForeignKey(fk => fk.OrganizationID);

            HasOptional(r => r.PreviousTeam)
                .WithMany(m => m.PreviousTeamNames)
                .HasForeignKey(fk => fk.PreviousTeamID);
        }
    }
}