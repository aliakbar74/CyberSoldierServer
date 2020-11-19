using System.Collections.Generic;

namespace CyberSoldierServer.Dtos.InsertDtos {
	public class PlayerDungeonInsertDto {
		public int DungeonId { get; set; }
		public ICollection<DungeonSlotInsertDto> Slots { get; set; }
		public ICollection<SlotDefenceItemInsertDto> DefenceItems { get; set; }
	}
}
