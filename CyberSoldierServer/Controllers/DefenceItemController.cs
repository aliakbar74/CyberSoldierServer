using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.InsertDtos;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class DefenceItemController : AuthApiController {
		private readonly CyberSoldierContext _dbContext;
		private readonly IMapper _mapper;

		public DefenceItemController(IMapper mapper, CyberSoldierContext dbContext) {
			_mapper = mapper;
			_dbContext = dbContext;
		}

		[HttpPost("AddDefenceItem")]
		public async Task<IActionResult> AddDefenceItem([FromBody] SlotDefenceItemInsertDto dto) {
			var dItem = _mapper.Map<SlotDefenceItem>(dto);
			await _dbContext.SlotDefenceItems.AddAsync(dItem);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("RemoveDefenceItem/{id}")]
		public async Task<IActionResult> RemoveDefenceItem(int id) {
			var dItem = await _dbContext.SlotDefenceItems.FirstOrDefaultAsync(d => d.Id == id);
			if (dItem == null)
				return NotFound("Defence item not found");

			_dbContext.SlotDefenceItems.Remove(dItem);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
