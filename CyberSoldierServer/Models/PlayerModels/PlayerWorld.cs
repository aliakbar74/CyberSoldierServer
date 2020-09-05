using System.Collections.Generic;

namespace CyberSoldierServer.Models.PlayerModels {
	public class PlayerWorld {
		public int Id { get; set; }

		public Player Player { get; set; }
		public int PlayerId { get; set; }

		public ICollection<PlayerWorldPath> Paths { get; set; }
	}
}
