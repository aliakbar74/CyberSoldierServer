using System.Linq;
using System.Threading.Tasks;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.EjectDtos;
using Microsoft.AspNetCore.Mvc;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class BaseModelController: AuthApiController {

		private readonly CyberSoldierContext _dbContext;

		public BaseModelController(CyberSoldierContext dbContext) {
			_dbContext = dbContext;
		}

		[HttpGet("GetBaseModels")]
		public async Task<IActionResult> GetBaseModels() {
			var baseModels = new BaseModels {
				Dungeons = _dbContext.Dungeons.ToList(),
				Cpus = _dbContext.Cpus.ToList(),
				Pools = _dbContext.Pools.ToList(),
				DefenceItems = _dbContext.DefenceItems.ToList()
			};

			return Ok(baseModels);
		}
	}
}
