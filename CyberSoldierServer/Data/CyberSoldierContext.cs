﻿using CyberSoldierServer.Models.Auth;
using CyberSoldierServer.Models.BaseModels;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Data {
	public class CyberSoldierContext : IdentityDbContext<AppUser, Role, int> {
		public CyberSoldierContext(DbContextOptions<CyberSoldierContext> options) : base(options) {
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(CyberSoldierContext).Assembly);
		}

		// base models
		public DbSet<Server> Servers { get; set; }
		public DbSet<Cpu> Cpus { get; set; }
		public DbSet<Dungeon> Dungeons { get; set; }
		public DbSet<Slot> Slots { get; set; }
		public DbSet<DefenceItem> DefenceItems { get; set; }
		public DbSet<Shield> Shields { get; set; }
		public DbSet<Weapon>  Weapons{ get; set; }
		public DbSet<Pool> Pools { get; set; }

		// user models
		public DbSet<Player> Players { get; set; }
		public DbSet<PlayerCamp> PlayerCamps { get; set; }
		public DbSet<CampCpu> ServerCpus { get; set; }
		public DbSet<GemPack> GemPacks { get; set; }
		public DbSet<TokenPack> TokenPacks { get; set; }
		public DbSet<CampDungeon> PlayerDungeons { get; set; }
		public DbSet<DungeonSlot> DungeonSlots { get; set; }
		public DbSet<SlotDefenceItem> SlotDefenceItems { get; set; }
		public DbSet<PlayerShield> PlayerShields { get; set; }
		public DbSet<PlayerWeapon> PlayerWeapons { get; set; }
		public DbSet<Attacker> Attackers { get; set; }
		public DbSet<CampPool> CampPools { get; set; }
	}
}
