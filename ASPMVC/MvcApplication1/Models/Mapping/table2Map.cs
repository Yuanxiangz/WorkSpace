using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcApplication1.Models.Mapping
{
    public class table2Map : EntityTypeConfiguration<table2>
    {
        public table2Map()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("table2");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
