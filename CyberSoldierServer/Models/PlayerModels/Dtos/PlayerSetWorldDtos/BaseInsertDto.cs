﻿using System.Collections.Generic;

namespace CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos {
	public class BaseInsertDto {
		public int ServerId { get; set; }
		public ICollection<DungeonInsertDto> Dungeons { get; set; }
		public ICollection<ServerCpuInsertDto> Cpus { get; set; }
	}
}
