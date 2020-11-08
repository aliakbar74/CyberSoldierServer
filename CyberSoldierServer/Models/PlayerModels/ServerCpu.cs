﻿using CyberSoldierServer.Models.BaseModels;

namespace CyberSoldierServer.Models.PlayerModels {
	public class ServerCpu {
		public int Id { get; set; }

		public int SlotId { get; set; }

		public Cpu Cpu { get; set; }
		public int CpuId { get; set; }

		public PlayerBase Base { get; set; }
		public int BaseId { get; set; }
	}
}
