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
		public async Task<IActionResult> AddDungeon([FromBody] PathDungeonInsertDto model) {
			if (!await _dbContext.PathDungeons.AnyAsync(p => p.Id == model.WorldPathId))
				return NotFound("Path not found");

			var dungeon = _mapper.Map<PathDungeon>(model);

			await _dbContext.PathDungeons.AddAsync(dungeon);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> UpgradeDungeon(int id) {
			var pathDungeon = await _dbContext.PathDungeons.Where(d => d.Id == id)
				.Include(d=>d.Dungeon).FirstOrDefaultAsync();
			if (pathDungeon==null)
				return NotFound("Dungeon not found");

			int level = pathDungeon.Dungeon.Level;
			var dungeon = await _dbContext.Dungeons.
				Where(d => d.DungeonType == pathDungeon.Dungeon.DungeonType && d.Level  == pathDungeon.Dungeon.Level+ 1)
				.FirstOrDefaultAsync();
			if (dungeon == null)
				return NotFound("Dungeon is at max level");

			pathDungeon.Dungeon = dungeon;
			_dbContext.PathDungeons.Update(pathDungeon);

			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
