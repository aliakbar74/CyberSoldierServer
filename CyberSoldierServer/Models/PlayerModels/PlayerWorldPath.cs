using System.Collections.Generic;
using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class PlayerWorldPath {
		public int Id { get; set; }

		public PlayerWorld World { get; set; }
		public int WorldId { get; set; }

		public Path Path { get; set; }
		public int PathId { get; set; }

		public Server Server { get; set; }
		public int ServerId { get; set; }

		public ICollection<ServerCpu> Cpus { get; set; }
		public ICollection<PathDungeon> Dungeons { get; set; }
	}
}
