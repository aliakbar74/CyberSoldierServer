using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberSoldierServer.Models.BaseModels {
	public class Slot {
		public int Id { get; set; }
		public DefenceType DefenceType { get; set; }
		public int Level { get; set; }

		public class SlotEntityConfiguration : IEntityTypeConfiguration<Slot> {
			public void Configure(EntityTypeBuilder<Slot> builder) {
				builder.Property(s => s.DefenceType).HasConversion<string>();
			}
		}
	}
}
