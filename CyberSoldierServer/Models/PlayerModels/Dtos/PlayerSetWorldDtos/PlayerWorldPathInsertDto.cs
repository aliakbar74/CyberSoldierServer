﻿using System.Collections.Generic;

namespace CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos {
	public class PlayerWorldPathInsertDto {
		public int PathId { get; set; }
		public int ServerId { get; set; }

		public ICollection<ServerCpuInsertDto> Cpus { get; set; }
		public ICollection<DungeonInsertDto> Dungeons { get; set; }
	}
}
