using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class PlayerWeapon {
		public int Id { get; set; }
		public Weapon Weapon { get; set; }
		public int WeaponId { get; set; }

		public Player Player { get; set; }
		public int PlayerId { get; set; }
	}
}
