using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CyberSoldierServer.Models.Auth;
using CyberSoldierServer.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberSoldierServer.Models.PlayerModels {
	public class Player {
		public int Id { get; set; }

		public AppUser User { get; set; }
		public int UserId { get; set; }

		public PlayerBase PlayerBase { get; set; }
		public int Gem { get; set; }
		public int Token { get; set; }
	}

	public class PlayerEntityConfiguration : IEntityTypeConfiguration<Player> {
		public void Configure(EntityTypeBuilder<Player> builder) {
			builder.HasIndex(x => x.UserId).IsUnique();
			builder.Property(p => p.Gem).IsRequired();
		}
	}
}
