using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class Attacker {
		public int Id { get; set; }
		public Player Player { get; set; }
		public int PlayerId { get; set; }
		public int AttackerPlayerId { get; set; }
		public int DungeonCount { get; set; }
		public CampCpu Cpu { get; set; }
		public int? CpuId { get; set; }
	}
}
