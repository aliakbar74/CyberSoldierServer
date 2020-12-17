using System.Collections.Generic;
using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class CampDungeon {
		public int Id { get; set; }
		public int PositionId { get; set; }

		public PlayerCamp Camp { get; set; }
		public int CampId { get; set; }

		public Dungeon Dungeon { get; set; }
		public int DungeonId { get; set; }

		public ICollection<DungeonSlot> Slots { get; set; }
	}
}
