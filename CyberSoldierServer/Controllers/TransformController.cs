using System.Threading.Tasks;
using CyberSoldierServer.Data;
using CyberSoldierServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TransformController : ControllerBase {
		private readonly ITransformRepo _repository;

		public TransformController(ITransformRepo repository) {
			_repository = repository;
		}

		[HttpGet]
		public async Task<ActionResult<TransformCollection>> GetTransforms() {
			if (!_repository.HaveTransforms()) return NotFound();
			var transforms = await _repository.GetTransforms();
			return Ok(transforms);
		}

		[HttpPost]
		public async Task<ActionResult> SetTransforms(TransformCollection transformCollection) {
			if (!_repository.HaveTransforms()) {
				await _repository.SetTransforms(transformCollection);
			} else {
				await _repository.UpdateTransforms(transformCollection);
			}

			return Ok();
		}
	}
}
