using System.Collections.Generic;

namespace CyberSoldierServer.Models.PlayerModels.Dtos.PlayerDtos {
	public class PlayerDto {
		public string UserName { get; set; }
		public int ServerId { get; set; }
		public ICollection<ServerCpuDto> Cpus { get; set; }
		public ICollection<PlayerDungeonDto> Dungeons { get; set; }
	}
}
