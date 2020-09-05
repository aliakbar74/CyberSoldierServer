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
		public DbSet<Path> Paths { get; set; }
		public DbSet<Server> Servers { get; set; }
		public DbSet<Cpu> Cpus { get; set; }
		public DbSet<Dungeon> Dungeons { get; set; }

		// user models
		public DbSet<Player> Players { get; set; }
		public DbSet<PlayerWorld> Worlds { get; set; }
		public DbSet<PlayerWorldPath> WorldPaths { get; set; }
		public DbSet<PathDungeon> PathDungeons { get; set; }
		public DbSet<ServerCpu> ServerCpus { get; set; }
	}
}
