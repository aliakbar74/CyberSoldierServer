using System;
using System.Collections.Generic;
using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class PlayerBase {
		public int Id { get; set; }

		public Player Player { get; set; }
		public int PlayerId { get; set; }

		public Server Server { get; set; }
		public int ServerId { get; set; }

		public ICollection<BaseDungeon> Dungeons { get; set; }
		public ICollection<ServerCpu> Cpus { get; set; }
	}
}
