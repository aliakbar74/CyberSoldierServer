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
		public async Task<ActionResult> SetWorld([FromBody] BaseInsertDto model) {
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

		// [HttpGet("{id}")]
		// [Authorize(Roles = "Admin")]
		// public async Task<IActionResult> GetWorld(int id) {
		// 	var player = await _dbContext.Players.Where(p => p.UserId == id)
		// 		.Include(p => p.User)
		// 		.Include(p=>p.PlayerBase)
		// 		.ThenInclude(p => p.Dungeons)
		// 		.ThenInclude(d=>d.Dungeon)
		// 		.Include(p=>p.PlayerBase)
		// 		.ThenInclude(p => p.Dungeons)
		// 		.ThenInclude(d => d.Slots)
		// 		.ThenInclude(s=>s.Slot)
		// 		.Include(p=>p.PlayerBase)
		// 		.ThenInclude(p => p.Dungeons)
		// 		.ThenInclude(d => d.DefenceItems)
		// 		.ThenInclude(d=>d.DefenceItem)
		// 		.Include(p=>p.PlayerBase)
		// 		.ThenInclude(p => p.Cpus)
		// 		.ThenInclude(c=>c.Cpu)
		// 		.Include(p=>p.PlayerBase)
		// 		.ThenInclude(p => p.Server)
		// 		.FirstOrDefaultAsync();
		// 	if (player == null)
		// 		return NotFound("player not found for this id");
		//
		// 	var playerDto = _mapper.Map<PlayerDto>(player);
		// 	playerDto.UserName = player.User.UserName;
		//
		// 	return Ok(playerDto);
		// }
	}
}
