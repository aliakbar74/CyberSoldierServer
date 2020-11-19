using System.Collections.Generic;

namespace CyberSoldierServer.Dtos.InsertDtos {
	public class CampInsertDto {
		public int ServerId { get; set; }
		public ICollection<DungeonInsertDto> Dungeons { get; set; }
		public ICollection<ServerCpuInsertDto> Cpus { get; set; }
	}
}
