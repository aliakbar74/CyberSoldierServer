namespace CyberSoldierServer.Dtos.EjectDtos {
	public class PlayerWeaponDto {
		public int Id { get; set; }
		public int BaseWeaponId { get; set; }
		public int PrefabId { get; set; }
		public int Type { get; set; }
		public int Level { get; set; }
	}

	public class PlayerShieldDto {
		public int Id{ get; set; }
		public int BaseShieldId { get; set; }
		public int Type { get; set; }
		public int Level { get; set; }
	}
}
