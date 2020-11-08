namespace CyberSoldierServer.Models.BaseModels {
	public class DungeonInfo {
		public int Id { get; set; }
		public int Level { get; set; }
		public Dungeon Dungeon { get; set; }
		public int DungeonId { get; set; }
	}
}
