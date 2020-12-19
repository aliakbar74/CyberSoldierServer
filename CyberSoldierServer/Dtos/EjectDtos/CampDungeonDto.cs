using System.Collections.Generic;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class CampDungeonDto {
		public int Id { get; set; }
		public int BaseDungeonId { get; set; }
		public int PrefabId { get; set; }
		public int DungeonType { get; set; }
		public int Level { get; set; }
		public int SlotCount { get; set; }
		public int PositionId { get; set; }
		public ICollection<DungeonSlotDto> Slots { get; set; }
	}
}
