using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class ServerCpu {
		public int Id { get; set; }

		public int SlotId { get; set; }

		public Cpu Cpu { get; set; }
		public int CpuId { get; set; }

		public PlayerWorldPath Path { get; set; }
		public int PathId { get; set; }
	}
}
