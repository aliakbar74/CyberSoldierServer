using System;
using System.Collections.Generic;
using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class PlayerCamp {
		public int Id { get; set; }

		public Player Player { get; set; }
		public int PlayerId { get; set; }

		public Server Server { get; set; }
		public int ServerId { get; set; }

		public ICollection<CampDungeon> Dungeons { get; set; }
		public ICollection<CampCpu> Cpus { get; set; }
	}
}
