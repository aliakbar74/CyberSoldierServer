using System.Collections.Generic;

namespace CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos {
	public class WorldInsertDto {
		public ICollection<PlayerWorldPathInsertDto> Paths { get; set; }
	}
}
