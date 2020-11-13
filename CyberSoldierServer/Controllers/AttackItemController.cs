using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.PlayerSetWorldDtos;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class AttackItemController : AuthApiController {
		private readonly IMapper _mapper;
		private readonly CyberSoldierContext _db;

		public AttackItemController(IMapper mapper, CyberSoldierContext db) {
			_mapper = mapper;
			_db = db;
		}

		[HttpPost("SetWeapon")]
		public async Task<IActionResult> SetWeapon([FromBody] WeaponDto weaponDto) {
			var player = await _db.Players.FirstOrDefaultAsync(p=>p.UserId==UserId);
			var weapon = _mapper.Map<PlayerWeapon>(weaponDto);

			if (await _db.PlayerWeapons.Where(w => w.PlayerId == player.Id).AnyAsync(w => w.WeaponId == weapon.Id)) {
				return BadRequest("You already have this weapon!");
			}

			weapon.PlayerId = player.Id;
			await _db.PlayerWeapons.AddAsync(weapon);
			await _db.SaveChangesAsync();
			return Ok();
		}
	}
}
