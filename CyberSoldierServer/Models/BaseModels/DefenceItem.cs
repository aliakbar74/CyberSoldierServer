using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberSoldierServer.Models.BaseModels {
	public class DefenceItem {
		public int Id { get; set; }
		public DefenceType DefenceType { get; set; }
		public int PrefabId { get; set; }
		public int Level { get; set; }
		public float Price { get; set; }

		public class DefenceItemEntityConfiguration : IEntityTypeConfiguration<DefenceItem> {
			public void Configure(EntityTypeBuilder<DefenceItem> builder) {
				builder.Property(d => d.DefenceType).HasConversion<string>();
			}
		}
	}
}
