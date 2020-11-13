using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class DungeonSlot {
		public int Id { get; set; }

		public Slot Slot { get; set; }
		public int SlotId { get; set; }

		public CampDungeon Dungeon { get; set; }
		public int DungeonId { get; set; }

		public SlotDefenceItem DefenceItem { get; set; }
	}
}
