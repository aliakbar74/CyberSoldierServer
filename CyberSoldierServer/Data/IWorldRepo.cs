using System.Threading.Tasks;
using CyberSoldierServer.Models;
using CyberSoldierServer.Models.BaseModels;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;

namespace CyberSoldierServer.Data {
	public interface IWorldRepo {
		bool HasWorld();
		Task<PlayerWorld> GetWorld(int playerId);
		Task SetWorld(int playerId, PlayerWorld playerWorld);
		Task AddDungeon(int playerId, int pathId, Dungeon dungeon);
		Task UpgradeServer(int playerId, int pathId);
		Task UpgradeDungeon(int playerId, int dungeonId);
		Task CpuUpgrade(int playerId, int cpuId);
	}
}
