namespace CyberSoldierServer.Models.BaseModels {
	public class Dungeon {
		public int Id { get; set; }
		public int PrefabId { get; set; }
		public int DungeonType { get; set; }
		public int Level { get; set; }
		public int SlotCount { get; set; }
		public float Price { get; set; }
	}
}
