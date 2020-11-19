using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.EjectDtos;
using CyberSoldierServer.Dtos.InsertDtos;
using CyberSoldierServer.Models.PlayerModels;
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

		[HttpPost("AddDungeon")]
		public async Task<IActionResult> AddDungeon([FromBody] DungeonInsertDto model) {
			var dungeon = _mapper.Map<CampDungeon>(model);
			var player = await _dbContext.Players.Where(p => p.UserId == UserId).Include(p=>p.Camp).FirstOrDefaultAsync();
			dungeon.CampId = player.Camp.Id;

			await _dbContext.PlayerDungeons.AddAsync(dungeon);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("UpgradeDungeon/{id}")]
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

		[HttpPost("RemoveDungeon/{id}")]
		public async Task<IActionResult> RemoveDungeon(int id) {
			var dungeon = await _dbContext.PlayerDungeons.FirstOrDefaultAsync(d => d.Id == id);
			if (dungeon == null)
				return NotFound("Dungeon not found");

			_dbContext.PlayerDungeons.Remove(dungeon);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		//todo : make body for this
		[HttpPost("ChangeDungeon")]
		public async Task<IActionResult> ChangeDungeon([FromHeader]int preId, [FromHeader]int nextId) {
			var dungeon = await _dbContext.PlayerDungeons.FirstOrDefaultAsync(d => d.Id == preId);
			if (dungeon == null)
				return NotFound("Dungeon not found");

			if (!await _dbContext.Dungeons.AnyAsync(d => d.Id == nextId))
				return NotFound("New Dungeon not found");

			dungeon.DungeonId = nextId;
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
