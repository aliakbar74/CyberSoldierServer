using System.Collections.Generic;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos;

namespace CyberSoldierServer.Models.PlayerModels.Dtos.PlayerDtos {
	public class WorldDto {
		public ICollection<WorldPathDto> Paths { get; set; }
	}
}
