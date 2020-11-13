using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Models.PlayerModels;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class DungeonController : AuthApiController {
		private readonly CyberSoldierContext _dbContext;
		private readonly IMapper _mapper;

		public DungeonController(CyberSoldierContext dbContext, IMapper mapper) {
			_dbContext = dbContext;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> AddDungeon([FromBody] DungeonInsertDto model) {
			var dungeon = _mapper.Map<BaseDungeon>(model);
			var player = await _dbContext.Players.Where(p => p.UserId == UserId).Include(p=>p.PlayerBase).FirstOrDefaultAsync();
			dungeon.BaseId = player.PlayerBase.Id;

			await _dbContext.PlayerDungeons.AddAsync(dungeon);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> UpgradeDungeon(int id) {
			var playerDungeon = await _dbContext.PlayerDungeons.Where(d => d.Id == id)
				.Include(d=>d.Dungeon).FirstOrDefaultAsync();
			if (playerDungeon==null)
				return NotFound("Dungeon not found");

			var dungeon = await _dbContext.Dungeons.
				Where(d => d.DungeonType == playerDungeon.Dungeon.DungeonType && d.Level  == playerDungeon.Dungeon.Level+ 1)
				.FirstOrDefaultAsync();
			if (dungeon == null)
				return NotFound("Dungeon is at max level");

			playerDungeon.Dungeon = dungeon;
			_dbContext.PlayerDungeons.Update(playerDungeon);

			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
