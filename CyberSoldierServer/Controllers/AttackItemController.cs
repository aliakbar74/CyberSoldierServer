﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.EjectDtos;
using CyberSoldierServer.Dtos.InsertDtos;
using CyberSoldierServer.Models.BaseModels;
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
		public async Task<IActionResult> SetWeapon([FromBody] WeaponInsertDto weaponDto) {
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

		[HttpGet("GetWeapons")]
		public async Task<ActionResult<ICollection<PlayerWeaponDto>>> GetWeapons() {
			var player = await _db.Players.Where(p => p.UserId == UserId).Include(p => p.Weapons).FirstOrDefaultAsync();
			if (player.Weapons.Count==0) {
				return NotFound("This player has no weapon");
			}

			var weapons = _mapper.Map<ICollection<PlayerWeaponDto>>(player.Weapons);
			return Ok(weapons);
		}

		[HttpPost("SetShield")]
		public async Task<IActionResult> SetShield([FromBody] ShieldInsertDto shieldDto) {
			var player = await _db.Players.FirstOrDefaultAsync(p=>p.UserId==UserId);
			var shield = _mapper.Map<PlayerShield>(shieldDto);

			if (await _db.PlayerShields.Where(s => s.PlayerId == player.Id).AnyAsync(s => s.ShieldId == shield.Id)) {
				return BadRequest("You already have this Shield!");
			}

			shield.PlayerId = player.Id;
			await _db.PlayerShields.AddAsync(shield);
			await _db.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("GetShields")]
		public async Task<ActionResult<ICollection<PlayerShieldDto>>> GetShields() {
			var player = await _db.Players.Where(p => p.UserId == UserId).Include(p => p.Shields).FirstOrDefaultAsync();
			if (player.Shields.Count==0) {
				return NotFound("This player has no Shield");
			}

			var shields = _mapper.Map<ICollection<PlayerShieldDto>>(player.Shields);
			return Ok(shields);
		}
	}
}
