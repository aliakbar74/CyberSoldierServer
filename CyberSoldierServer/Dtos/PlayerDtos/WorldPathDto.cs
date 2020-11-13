using System.Collections.Generic;

namespace CyberSoldierServer.Dtos.PlayerDtos {
	public class WorldPathDto {

		public int PathId { get; set; }
		public int ServerId { get; set; }

		public ICollection<ServerCpuDto> Cpus { get; set; }
		public ICollection<PathDungeonDto> Dungeons { get; set; }
	}
}
