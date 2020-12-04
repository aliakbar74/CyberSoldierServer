using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.EjectDtos;
using CyberSoldierServer.Dtos.InsertDtos;
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

			var camp = await _dbContext.PlayerCamps.FirstOrDefaultAsync(c => c.PlayerId == player.Id);
			if (camp != null)
				_dbContext.PlayerCamps.Remove(camp);

			var playerBase = _mapper.Map<PlayerCamp>(model);
			playerBase.PlayerId = player.Id;

			await _dbContext.PlayerCamps.AddAsync(playerBase);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpGet("GetWorld")]
		public async Task<IActionResult> GetWorld() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			var camp = await _dbContext.PlayerCamps.Where(c => c.PlayerId == player.Id)
				.Include(p => p.Dungeons)
				.ThenInclude(d=>d.Dungeon)
				.Include(p=>p.Dungeons)
				.ThenInclude(d => d.Slots)
				.ThenInclude(s=>s.Slot)
				.Include(p=>p.Dungeons)
				.ThenInclude(d => d.Slots)
				.ThenInclude(s => s.DefenceItem)
				.Include(p => p.Cpus)
				.ThenInclude(c=>c.Cpu)
				.FirstOrDefaultAsync();
			if (camp == null)
				return NotFound("Camp not found for this id");

			var playerDto = _mapper.Map<PlayerCampDto>(camp);

			return Ok(playerDto);
		}
	}
}
