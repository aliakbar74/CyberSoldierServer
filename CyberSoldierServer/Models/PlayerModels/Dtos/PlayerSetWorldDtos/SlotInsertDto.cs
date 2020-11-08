namespace CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos {
	public class SlotInsertDto {
		public int SlotId { get; set; }
		public int DungeonId { get; set; }
		public SlotDefenceItemInsertDto DefenceItem { get; set; }
	}
}
