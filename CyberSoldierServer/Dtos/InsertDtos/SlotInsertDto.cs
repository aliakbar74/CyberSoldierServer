namespace CyberSoldierServer.Dtos.InsertDtos {
	public class SlotInsertDto {
		public int SlotId { get; set; }
		public int DungeonId { get; set; }
		public SlotDefenceItemInsertDto DefenceItem { get; set; }
	}
}
