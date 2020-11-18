using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.PlayerSetWorldDtos;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Authorization;
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
		public async Task<ActionResult> SetWorld([FromBody] CampInsertDto model) {
			var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.UserId == UserId);

			if (player == null)
				return NotFound("player not found for this user");

			if (await _dbContext.PlayerCamps.AnyAsync(x => x.PlayerId == player.Id))
				return BadRequest("you already have world");

			var playerBase = _mapper.Map<PlayerCamp>(model);
			playerBase.PlayerId = player.Id;

			// player.PlayerBase.ServerId = playerBase.ServerId;
			_dbContext.PlayerCamps.Update(playerBase);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetWorld() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			var camp = await _dbContext.PlayerCamps.Where(c => c.PlayerId == player.Id)
				.Include(p => p.Dungeons)
				.ThenInclude(d => d.Slots)
				.ThenInclude(s => s.DefenceItem)
				.Include(p => p.Cpus)
				.FirstOrDefaultAsync();
			if (camp == null)
				return NotFound("Camp not found for this id");

			var playerDto = _mapper.Map<CampInsertDto>(camp);

			return Ok(playerDto);
		}
	}
}
