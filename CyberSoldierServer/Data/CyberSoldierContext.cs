using CyberSoldierServer.Models.Auth;
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
		// public DbSet<Path> Paths { get; set; }
		public DbSet<Server> Servers { get; set; }
		public DbSet<Cpu> Cpus { get; set; }
		public DbSet<Dungeon> Dungeons { get; set; }
		public DbSet<Slot> Slots { get; set; }
		public DbSet<DefenceItem> DefenceItems { get; set; }

		// user models
		public DbSet<Player> Players { get; set; }
		public DbSet<PlayerBase> PlayerBases { get; set; }
		public DbSet<ServerCpu> ServerCpus { get; set; }
		public DbSet<GemPack> GemPacks { get; set; }
		public DbSet<TokenPack> TokenPacks { get; set; }
		public DbSet<BaseDungeon> PlayerDungeons { get; set; }
		public DbSet<DungeonSlot> DungeonSlots { get; set; }
		public DbSet<SlotDefenceItem> SlotDefenceItems { get; set; }
	}
}
