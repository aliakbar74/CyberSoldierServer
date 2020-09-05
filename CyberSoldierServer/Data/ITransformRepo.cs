using System.Collections.Generic;
using System.Threading.Tasks;
using CyberSoldierServer.Models;

namespace CyberSoldierServer.Data {
	public interface ITransformRepo {
		Task<TransformCollection> GetTransforms();
		Task SetTransforms(TransformCollection transforms);
		Task UpdateTransforms(TransformCollection transforms);
		Task DeleteTransforms();
		bool HaveTransforms();
	}
}
