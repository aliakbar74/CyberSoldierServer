using System.Collections.Generic;
using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class BaseDungeon {
		public int Id { get; set; }

		public PlayerBase Base { get; set; }
		public int BaseId { get; set; }

		public Dungeon Dungeon { get; set; }
		public int DungeonId { get; set; }

		public ICollection<DungeonSlot> Slots { get; set; }
	}
}
