using System.IO;
using System.Threading.Tasks;
using CyberSoldierServer.Models;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CyberSoldierServer.Data {
	public class TextTransformRepo : ITransformRepo {
		const string PATH = @"H:\Prj\GameServer\CallApi\Data\ObjectTransforms.txt";

		private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions {
			PropertyNameCaseInsensitive = true
		};

		public async Task<TransformCollection> GetTransforms() {
			string json;
			using (var reader = File.OpenText(PATH)) {
				json = await reader.ReadToEndAsync();
			}

			// var transCollection = JsonSerializer.Deserialize<TransformCollection>(json, _serializerOptions);
			var transCollection = JsonConvert.DeserializeObject<TransformCollection>(json);
			return transCollection;
		}

		public async Task SetTransforms(TransformCollection transforms) {
			using (var writer = File.CreateText(PATH)) {
				var json = JsonSerializer.Serialize(transforms);
				await writer.WriteLineAsync(json);
			}
		}

		public Task UpdateTransforms(TransformCollection transforms) {
			throw new System.NotImplementedException();
		}

		public Task DeleteTransforms() {
			throw new System.NotImplementedException();
		}

		public bool HaveTransforms() {
			return File.Exists(PATH);
		}
	}
}
