using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CIEDigitalLib.Models.Binding;

namespace CIEDigitalLib.Models.Mapping
{
    public class CombineMap : EntityTypeConfiguration<Combine>
    {
        public CombineMap()
        {
            ToTable("Combine");

            Property(t => t.ID)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasParameterName("ID")
                .IsRequired();

            Property(t => t.Arms)
                .HasColumnName("Arms")
                .HasColumnOrder(2)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Arms")
                .IsRequired();

            Property(t => t.Bench)
                .HasColumnName("Bench")
                .HasColumnOrder(3)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Bench")
                .IsRequired();

            Property(t => t.Broad)
                .HasColumnName("Broad")
                .HasColumnOrder(4)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Broad")
                .IsRequired();

            Property(t => t.College)
                .HasColumnName("College")
                .HasColumnOrder(5)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("College")
                .IsRequired();

            Property(t => t.FirstName)
                .HasColumnName("FirstName")
                .HasColumnOrder(6)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("FirstName")
                .IsRequired();

            Property(t => t.FourtyYardDash)
                .HasColumnName("FourtyYardDash")
                .HasColumnOrder(7)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("FourtyYardDash")
                .IsRequired();

            Property(t => t.Hands)
                .HasColumnName("Hands")
                .HasColumnOrder(8)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Hands")
                .IsRequired();

            Property(t => t.HeightFeet)
                .HasColumnName("HeightFeet")
                .HasColumnOrder(9)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("HeightFeet")
                .IsRequired();

            Property(t => t.HeightInches)
                .HasColumnName("HeightInches")
                .HasColumnOrder(10)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("HeightInches")
                .IsRequired();

            Property(t => t.HeightInchesTotal)
                .HasColumnName("HeightInchesTotal")
                .HasColumnOrder(11)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("HeightInchesTotal")
                .IsRequired();

            Property(t => t.LastName)
                .HasColumnName("LastName")
                .HasColumnOrder(12)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("LastName")
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnOrder(13)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(32)
                .HasParameterName("Name")
                .IsRequired();

            Property(t => t.NFLGrade)
                .HasColumnName("NFLGrade")
                .HasColumnOrder(14)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("NFLGrade")
                .IsRequired();

            Property(t => t.Pick)
                .HasColumnName("Pick")
                .HasColumnOrder(15)
                .HasColumnType("NVARCHAR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasMaxLength(10)
                .HasParameterName("Pick")
                .IsRequired();

            Property(t => t.PickRound)
                .HasColumnName("PickRound")
                .HasColumnOrder(16)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("PickRound")
                .IsRequired();

            Property(t => t.PickTotal)
                .HasColumnName("PickTotal")
                .HasColumnOrder(17)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("PickTotal")
                .IsRequired();

            Property(t => t.PositionID)
                .HasColumnName("PositionID")
                .HasColumnOrder(18)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("PositionID")
                .IsRequired();

            Property(t => t.Round)
                .HasColumnName("Round")
                .HasColumnOrder(19)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Round")
                .IsRequired();

            Property(t => t.TenYardDash)
                .HasColumnName("TenYardDash")
                .HasColumnOrder(20)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("TenYardDash")
                .IsRequired();

            Property(t => t.ThreeCone)
                .HasColumnName("ThreeCone")
                .HasColumnOrder(21)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("ThreeCone")
                .IsRequired();

            Property(t => t.TwentyYardDash)
                .HasColumnName("TwentyYardDash")
                .HasColumnOrder(22)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("TwentyYardDash")
                .IsRequired();

            Property(t => t.TwentyYardShuttle)
                .HasColumnName("TwentyYardShuttle")
                .HasColumnOrder(23)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("TwentyYardShuttle")
                .IsRequired();

            Property(t => t.Vertical)
                .HasColumnName("Vertical")
                .HasColumnOrder(24)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Vertical")
                .IsRequired();

            Property(t => t.WeightPounds)
                .HasColumnName("WeightPounds")
                .HasColumnOrder(25)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("WeightPounds")
                .IsRequired();

            Property(t => t.Wonderlic)
                .HasColumnName("Wonderlic")
                .HasColumnOrder(26)
                .HasColumnType("DECIMAL")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Wonderlic")
                .IsRequired();

            Property(t => t.Year)
                .HasColumnName("Year")
                .HasColumnOrder(27)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasParameterName("Year")
                .IsRequired();

            HasKey(t => t.ID);

            HasRequired(r => r.Position)
                .WithMany(m => m.Combines)
                .HasForeignKey(fk => fk.PositionID);
        }
    }
}