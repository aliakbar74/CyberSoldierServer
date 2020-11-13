using System.Collections.Generic;

namespace CyberSoldierServer.Dtos.PlayerDtos {
	public class PlayerDungeonDto {
		public int DungeonId { get; set; }
		public ICollection<DungeonSlotDto> Slots { get; set; }
		public ICollection<SlotDefenceItemDto> DefenceItems { get; set; }
	}
}
