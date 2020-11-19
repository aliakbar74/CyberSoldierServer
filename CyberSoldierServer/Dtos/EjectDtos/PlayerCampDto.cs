using System.Collections.Generic;
using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class PlayerCampDto {
		public int Id { get; set; }
		public Server Server { get; set; }
		public ICollection<CampDungeonDto> Dungeons { get; set; }
		public ICollection<CampCpuDto> Cpus { get; set; }
	}

	public class CampDungeonDto {
		public int Id { get; set; }
		public int BaseDungeonId { get; set; }
		public int PrefabId { get; set; }
		public int DungeonType { get; set; }
		public int Level { get; set; }
		public int SlotCount { get; set; }
		public ICollection<DungeonSlotDto> Slots { get; set; }
	}

	public class DungeonSlotDto {
		public int Id { get; set; }
		public int BaseSlotId { get; set; }
		public int Level { get; set; }
		public DefenceType DefenceType { get; set; }
		public SlotDefenceItemDto DefenceItem { get; set; }
	}

	public class SlotDefenceItemDto {
		public int Id { get; set; }
		public int BaseDefenceItemId { get; set; }
		public int PrefabId { get; set; }
		public int Level { get; set; }
		public DefenceType DefenceType { get; set; }
	}

	public class CampCpuDto {
		public int Id { get; set; }
		public int BaseCpuId { get; set; }
		public int SlotId { get; set; }
		public int CpuType { get; set; }
		public int Level { get; set; }
	}
}
