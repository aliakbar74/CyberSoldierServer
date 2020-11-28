using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class SlotDefenceItemDto {
		public int Id { get; set; }
		public int BaseDefenceItemId { get; set; }
		public int PrefabId { get; set; }
		public int Level { get; set; }
		public DefenceType DefenceType { get; set; }
	}
}
