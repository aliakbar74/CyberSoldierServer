using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.InsertDtos;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class PoolController : AuthApiController {

		private readonly CyberSoldierContext _dbContext;
		private readonly IMapper _mapper;

		public PoolController(CyberSoldierContext dbContext, IMapper mapper) {
			_dbContext = dbContext;
			_mapper = mapper;
		}

		[HttpPost("AddPool")]
		public async Task<IActionResult> AddPool([FromBody] CampPoolInsertDto dto) {
			var player = await _dbContext.Players
				.Where(p => p.UserId == UserId)
				.Include(p=>p.Camp)
				.FirstOrDefaultAsync();
			var pool = _mapper.Map<CampPool>(dto);
			pool.CampId = player.Camp.Id;

			await _dbContext.CampPools.AddAsync(pool);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("RemovePool/{id}")]
		public async Task<IActionResult> RemovePool(int id) {
			var pool = await _dbContext.CampPools.FirstOrDefaultAsync(p => p.Id == id);
			if (pool == null)
				return NotFound("pool not found");

			_dbContext.CampPools.Remove(pool);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
