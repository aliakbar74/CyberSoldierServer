using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class PlayerShield {
		public int Id { get; set; }
		public Shield  Shield{ get; set; }
		public int ShieldId { get; set; }
		public Player Player { get; set; }
		public int PlayerId { get; set; }
	}
}
