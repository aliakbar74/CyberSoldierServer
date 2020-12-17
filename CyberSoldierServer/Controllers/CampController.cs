using System;
using System.Collections.Generic;
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

			// var camp = await _dbContext.PlayerCamps.FirstOrDefaultAsync(c => c.PlayerId == player.Id);

			var playerBase = _mapper.Map<PlayerCamp>(model);
			playerBase.PlayerId = player.Id;

			// if (camp != null) {
			// 	playerBase.LastCollectTime = DateTime.Now;
			// 	_dbContext.PlayerCamps.Remove(camp);
			// }

			await _dbContext.PlayerCamps.AddAsync(playerBase);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpGet("GetWorld")]
		public async Task<IActionResult> GetWorld() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);

			await Task.Run(() => RedirectToAction(nameof(GetServerToken)));

			var camp = await _dbContext.PlayerCamps.Where(c => c.PlayerId == player.Id)
				.Include(p => p.Dungeons)
				.ThenInclude(d => d.Dungeon)
				.Include(p => p.Dungeons)
				.ThenInclude(d => d.Slots)
				.ThenInclude(s => s.Slot)
				.Include(p => p.Dungeons)
				.ThenInclude(d => d.Slots)
				.ThenInclude(s => s.DefenceItem)
				.ThenInclude(d => d.DefenceItem)
				.Include(p => p.Cpus)
				.ThenInclude(c => c.Cpu)
				.Include(c => c.Pools)
				.ThenInclude(p => p.Pool)
				.FirstOrDefaultAsync();
			if (camp == null)
				return NotFound("Camp not found for this id");

			var playerDto = _mapper.Map<PlayerCampDto>(camp);

			return Ok(playerDto);
		}

		[HttpPost("UpdateWorld")]
		public async Task<IActionResult> UpdateWorld([FromBody] CampInsertDto model) {
			var player = await _dbContext.Players
				.Where(x => x.UserId == UserId)
				.Include(p=>p.Camp)
				.FirstOrDefaultAsync();

			if (player == null)
				return NotFound("player not found for this user");

			player.Camp = _mapper.Map<PlayerCamp>(model);
			player.Camp.LastCollectTime = DateTime.Now;

			_dbContext.PlayerCamps.Update(player.Camp);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpGet("GetServerToken")]
		public async Task<ActionResult<ServerTokenDto>> GetServerToken() {
			//todo : Moein
			var player = await _dbContext.Players
				.Where(p => p.UserId == UserId)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Server)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Cpus)
				.ThenInclude(c => c.Cpu)
				.Include(p => p.Camp.Cpus)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Pools)
				.ThenInclude(p => p.Pool)
				.FirstOrDefaultAsync();

			var valueInPools = player.Camp.Pools.Sum(p => p.CurrentValue);
			if (valueInPools > player.Camp.Pools.Sum(p => p.Pool.Capacity))
				return BadRequest("Pools are full");

			var elapsedTime = DateTime.Now.Subtract(player.Camp.LastCollectTime).Minutes;
			var value = player.Camp.Cpus.Select(c => c.Cpu.Power).Sum(power => elapsedTime * power);

			foreach (var pool in player.Camp.Pools) {
				if (pool.CurrentValue + value < pool.Pool.Capacity) {
					pool.CurrentValue += value;
					break;
				}

				var empty = pool.Pool.Capacity - pool.CurrentValue;
				pool.CurrentValue = pool.Pool.Capacity;
				value -= empty;
			}

			player.Camp.LastCollectTime = DateTime.Now;

			var serverToken = new ServerTokenDto {
				ServerCapacity = player.Camp.Server.Capacity,
				ServerCurrentValue = player.Token,
				Pools = _mapper.Map<ICollection<CampPoolDto>>(player.Camp.Pools)
			};

			_dbContext.Players.Update(player);
			await _dbContext.SaveChangesAsync();

			return Ok(serverToken);
		}

		[HttpPost("CollectToken/{id}")]
		public async Task<IActionResult> CollectToken(int id) {
			var player = await _dbContext.Players
				.Where(p => p.UserId == UserId)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Pools)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Server)
				.FirstOrDefaultAsync();

			var pool = player.Camp.Pools.First(p => p.Id == id);
			if (pool == null)
				return NotFound("Pool not found");

			if (player.Camp.Server.Capacity <= player.Token)
				return BadRequest("Server is full");

			if (player.Camp.Server.Capacity < player.Token + pool.CurrentValue) {
				var empty = player.Camp.Server.Capacity - player.Token;
				player.Token = player.Camp.Server.Capacity;
				pool.CurrentValue -= empty;
			} else {
				player.Token += pool.CurrentValue;
				pool.CurrentValue = 0;
			}

			_dbContext.CampPools.Update(pool);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
