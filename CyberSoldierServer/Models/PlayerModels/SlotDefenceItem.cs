using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class SlotDefenceItem {
		public int Id { get; set; }

		public DungeonSlot Slot { get; set; }
		public int SlotId { get; set; }

		public DefenceItem DefenceItem { get; set; }
		public int DefenceItemId { get; set; }
	}
}
