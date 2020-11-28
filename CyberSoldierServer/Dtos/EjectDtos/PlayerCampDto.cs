using System.Collections.Generic;
using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class PlayerCampDto {
		public int Id { get; set; }
		public Server Server { get; set; }
		public ICollection<CampDungeonDto> Dungeons { get; set; }
		public ICollection<CampCpuDto> Cpus { get; set; }
	}
}
