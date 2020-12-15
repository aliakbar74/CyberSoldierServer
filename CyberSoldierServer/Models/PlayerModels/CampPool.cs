using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class CampPool {
		public int Id { get; set; }
		public int CurrentValue { get; set; }

		public Pool Pool { get; set; }
		public int PoolId { get; set; }

		public PlayerCamp Camp { get; set; }
		public int CampId { get; set; }
	}
}
