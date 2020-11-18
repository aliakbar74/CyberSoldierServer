using System.Collections.Generic;
using CyberSoldierServer.Dtos.PlayerSetWorldDtos;

namespace CyberSoldierServer.Dtos.PlayerDtos {
	public class PlayerDto {
		public string UserName { get; set; }
		public int Level { get; set; }
		public CampInsertDto Camp { get; set; }
	}
}
