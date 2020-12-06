using System.Collections.Generic;
using CyberSoldierServer.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberSoldierServer.Models.PlayerModels {
	public class Player {
		public int Id { get; set; }
		public AppUser User { get; set; }
		public int UserId { get; set; }
		public bool IsOnline { get; set; }
		public int Level { get; set; }
		public PlayerCamp Camp { get; set; }
		public uint Gem { get; set; }
		public uint Token { get; set; }
		public ICollection<PlayerWeapon> Weapons { get; set; }
		public ICollection<PlayerShield> Shields { get; set; }
		public ICollection<Attacker> Attackers { get; set; }
	}

	public class PlayerEntityConfiguration : IEntityTypeConfiguration<Player> {
		public void Configure(EntityTypeBuilder<Player> builder) {
			builder.HasIndex(x => x.UserId).IsUnique();
			builder.Property(p => p.Gem).IsRequired();
		}
	}
}
