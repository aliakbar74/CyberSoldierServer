using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class PathDungeon {
		public int Id { get; set; }

		public PlayerWorldPath WorldPath { get; set; }
		public int WorldPathId { get; set; }

		public Dungeon Dungeon { get; set; }
		public int DungeonId { get; set; }
	}
}
