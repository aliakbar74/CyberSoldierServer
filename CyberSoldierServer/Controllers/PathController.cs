using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Models.PlayerModels;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {

	[Route("api/[controller]")]
	public class PathController : AuthApiController {
		private readonly CyberSoldierContext _dbContext;
		private readonly IMapper _mapper;

		public PathController(IMapper mapper, CyberSoldierContext dbContext) {
			_mapper = mapper;
			_dbContext = dbContext;
		}

		[HttpPost]
		public async Task<IActionResult> AddPath([FromBody] PlayerWorldPathInsertDto model) {

			var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.UserId == UserId);

			if (player==null)
				return NotFound("player not found");

			var world = await _dbContext.Worlds.FirstOrDefaultAsync(w => w.PlayerId == player.Id);
			var path = _mapper.Map<PlayerWorldPath>(model);
			path.WorldId = world.Id;

			await _dbContext.WorldPaths.AddAsync(path);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
