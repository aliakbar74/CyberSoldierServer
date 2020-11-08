using System;

namespace CyberSoldierServer.Models.BaseModels {
	public class Server {
		public int Id { get; set; }
		public int CpuCount { get; set; }
		public uint Capacity { get; set; }
	}
}
