using System.Collections.Generic;
using CyberSoldierServer.Dtos.InsertDtos;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class WorldPathDto {

		public int PathId { get; set; }
		public int ServerId { get; set; }

		public ICollection<ServerCpuInsertDto> Cpus { get; set; }
		public ICollection<PathDungeonDto> Dungeons { get; set; }
	}
}
