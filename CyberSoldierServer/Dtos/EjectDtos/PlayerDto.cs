using CyberSoldierServer.Dtos.InsertDtos;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class PlayerDto {
		public string UserName { get; set; }
		public int Level { get; set; }
		public PlayerCampDto Camp { get; set; }
	}
}
