namespace CyberSoldierServer.Settings {
	public class JwtSettings {
		public string Issuer { get; set; }
		public string Key { get; set; }
		public int Expire { get; set; }
	}
}
