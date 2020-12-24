using System.Collections.Generic;
using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class BaseModels {
		public ICollection<Dungeon> Dungeons { get; set; }
		public ICollection<Cpu> Cpus { get; set; }
		public ICollection<DefenceItem> DefenceItems { get; set; }
		public ICollection<Pool> Pools { get; set; }
	}
}
