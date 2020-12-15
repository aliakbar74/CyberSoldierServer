using System.Collections.Generic;

namespace CyberSoldierServer.Dtos.EjectDtos {
	public class ServerTokenDto {
		public int ServerCurrentValue { get; set; }
		public int ServerCapacity { get; set; }
		public ICollection<CampPoolDto> Pools { get; set; }
	}
}
