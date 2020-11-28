using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class DungeonSlotDto {
		public int Id { get; set; }
		public int BaseSlotId { get; set; }
		public int Level { get; set; }
		public DefenceType DefenceType { get; set; }
		public SlotDefenceItemDto DefenceItem { get; set; }
	}
}
