using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Models.PlayerModels;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerDtos;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class WorldController : AuthApiController {
		private readonly CyberSoldierContext _dbContext;
		private readonly IMapper _mapper;

		public WorldController(CyberSoldierContext dbContext, IMapper mapper) {
			_dbContext = dbContext;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<ActionResult> SetWorld([FromBody] WorldInsertDto model) {
			var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.UserId == UserId);

			if (player == null)
				return NotFound("player not found for this user");

			if (await _dbContext.Worlds.AnyAsync(x => x.PlayerId == player.Id)) {
				return BadRequest("you already have world");
			}

			var playerWorld = _mapper.Map<PlayerWorld>(model);
			playerWorld.PlayerId = player.Id;

			await _dbContext.Worlds.AddAsync(playerWorld);

			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetWorld(int id) {
			var player = await _dbContext.Players.Where(p => p.UserId == id)
				.Include(p=>p.User)
				.Include(p => p.World)
				.ThenInclude(w => w.Paths)
				.ThenInclude(p => p.Dungeons)
				.ThenInclude(d=>d.Dungeon)
				.Include(p => p.World)
				.ThenInclude(w => w.Paths)
				.ThenInclude(p => p.Cpus)
				.ThenInclude(c=>c.Cpu)
				.Include(p => p.World)
				.ThenInclude(w => w.Paths)
				.ThenInclude(p => p.Server)
				.Include(p => p.World)
				.ThenInclude(w => w.Paths)
				.ThenInclude(p => p.Path)
				.FirstOrDefaultAsync();
			if (player == null)
				return NotFound("player not found for this id");

			var playerDto = new PlayerDto {
				UserName = player.User.UserName,
				World = _mapper.Map<WorldDto>(player.World)
			};

			return Ok(playerDto);
		}
	}
}
