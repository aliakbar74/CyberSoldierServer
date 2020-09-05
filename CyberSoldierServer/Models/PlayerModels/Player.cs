using CyberSoldierServer.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberSoldierServer.Models.PlayerModels {
	public class Player {
		public int Id { get; set; }

		public AppUser User { get; set; }
		public int UserId { get; set; }

		public PlayerWorld World { get; set; }
	}

	public class PlayerEntityConfiguration : IEntityTypeConfiguration<Player> {
		public void Configure(EntityTypeBuilder<Player> builder) {
			builder.HasIndex(x => x.UserId).IsUnique();
		}
	}
}
