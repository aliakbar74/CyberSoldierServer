using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class CampCpu {
		public int Id { get; set; }

		public int SlotId { get; set; }

		public Cpu Cpu { get; set; }
		public int CpuId { get; set; }

		public PlayerCamp Camp { get; set; }
		public int CampId { get; set; }
	}
}
