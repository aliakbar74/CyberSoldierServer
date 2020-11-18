using System.Collections.Generic;
using CyberSoldierServer.Dtos.PlayerDtos;

namespace CyberSoldierServer.Dtos.PlayerSetWorldDtos {
	public class PlayerWorldPathInsertDto {
		public int PathId { get; set; }
		public int ServerId { get; set; }

		public ICollection<ServerCpuDto> Cpus { get; set; }
		public ICollection<DungeonInsertDto> Dungeons { get; set; }
	}
}
