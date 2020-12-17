using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.InsertDtos;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class SlotController : AuthApiController {

		private readonly CyberSoldierContext _dbContext;
		private readonly IMapper _mapper;

		public SlotController(CyberSoldierContext dbContext, IMapper mapper) {
			_dbContext = dbContext;
			_mapper = mapper;
		}

		[HttpPost("AddSlot")]
		public async Task<IActionResult> AddSlot([FromBody] SlotInsertDto dto) {
			var slot = _mapper.Map<DungeonSlot>(dto);
			await _dbContext.DungeonSlots.AddAsync(slot);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("RemoveSlot/{id}")]
		public async Task<IActionResult> RemoveSlot(int id) {
			var slot = await _dbContext.DungeonSlots.FirstOrDefaultAsync(s => s.Id == id);
			if (slot == null)
				return NotFound("Slot not found");

			_dbContext.DungeonSlots.Remove(slot);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
