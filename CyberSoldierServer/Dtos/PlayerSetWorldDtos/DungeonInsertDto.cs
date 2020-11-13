using System.Collections.Generic;

namespace CyberSoldierServer.Dtos.PlayerSetWorldDtos {
	public class DungeonInsertDto {
		public int DungeonId { get; set; }
		public ICollection<SlotInsertDto> Slots { get; set; }
	}
}
