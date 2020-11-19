using System.Collections.Generic;

namespace CyberSoldierServer.Dtos.InsertDtos {
	public class DungeonInsertDto {
		public int DungeonId { get; set; }
		public ICollection<SlotInsertDto> Slots { get; set; }
	}
}
