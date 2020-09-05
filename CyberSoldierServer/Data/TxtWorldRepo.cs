using System.IO;
using System.Threading.Tasks;
using CyberSoldierServer.Models;
using CyberSoldierServer.Models.BaseModels;
using CyberSoldierServer.Models.PlayerModels;
using Newtonsoft.Json;

namespace CyberSoldierServer.Data {
	public class TxtWorldRepo : IWorldRepo {
		const string PATH = @"H:\Prj\GameServer\CallApi\Data\ObjectTransforms.txt";

		// private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
		// {
		//     PropertyNameCaseInsensitive = true
		// };

		public bool HasWorld() {
			return File.Exists(PATH);
		}

		public async Task<PlayerWorld> GetWorld(int id) {
			string json;
			using (var reader = File.OpenText(PATH)) {
				json = await reader.ReadToEndAsync();
			}

			var world = JsonConvert.DeserializeObject<PlayerWorld>(json);
			return world;
		}

		public async Task SetWorld(int playerId, PlayerWorld playerWorld) {
			await using var writer = File.CreateText(PATH);
			var json = JsonConvert.SerializeObject(playerWorld);
			await writer.WriteLineAsync(json);
		}

		public Task AddDungeon(int playerId, int pathId, Dungeon dungeon) {
			throw new System.NotImplementedException();
		}

		public Task UpgradeServer(int playerId, int pathId) {
			throw new System.NotImplementedException();
		}

		public Task UpgradeDungeon(int playerId, int dungeonId) {
			throw new System.NotImplementedException();
		}

		public Task CpuUpgrade(int playerId, int cpuId) {
			throw new System.NotImplementedException();
		}
	}
}
