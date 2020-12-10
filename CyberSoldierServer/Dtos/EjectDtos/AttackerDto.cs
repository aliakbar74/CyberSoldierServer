namespace CyberSoldierServer.Dtos.EjectDtos {
	public class AttackerDto {
		public int AttackerId { get; set; }
		public string UserName { get; set; }
		public int Level { get; set; }
		public int DungeonCount { get; set; }
		public CampCpuDto Cpu { get; set; }
	}
}
