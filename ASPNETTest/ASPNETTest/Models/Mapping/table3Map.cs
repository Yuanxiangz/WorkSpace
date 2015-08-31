using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ASPNETTest.Models.Mapping
{
	public class table3Map : EntityTypeConfiguration<table3>
	{
		public table3Map()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.ID)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(10);

			this.Property(t => t.Class)
				.HasMaxLength(50);

			// Table & Column Mappings
			this.ToTable("table3");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Class).HasColumnName("Class");
		}
	}
}
