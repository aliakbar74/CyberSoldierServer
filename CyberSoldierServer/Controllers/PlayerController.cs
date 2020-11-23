using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.EjectDtos;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class PlayerController : AuthApiController {
		private readonly CyberSoldierContext _dbContext;
		private readonly IMapper _mapper;

		public PlayerController(CyberSoldierContext dbContext, IMapper mapper) {
			_dbContext = dbContext;
			_mapper = mapper;
		}

		[HttpPost("AddGems/{gemId}")]
		public async Task<IActionResult> AddGem(int gemId) {
			var gemPack = await _dbContext.GemPacks.FirstOrDefaultAsync(g => g.Id == gemId);
			if (gemPack == null)
				return NotFound("Gem packs with this id not found");

			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);

			if (player == null)
				return NotFound("Player not found");

			player.Gem += gemPack.Value;

			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("AddGemsUnLimit/{value}")]
		public async Task<IActionResult> AddGemUnLimit(uint value) {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (player == null)
				return NotFound("Player not found");

			player.Gem += value;

			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("AddTokenUnLimit/{value}")]
		public async Task<IActionResult> AddTokenUnLimit(uint value) {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (player == null)
				return NotFound("Player not found");

			player.Token += value;

			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("GetGems")]
		public async Task<IActionResult> GetGem() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (player == null)
				return NotFound("Player not found");

			var gem = player.Gem;
			return Ok(new {gem});
		}

		[HttpPost("AddTokens/{tokenId}")]
		public async Task<IActionResult> AddToken(int tokenId) {
			var tokenPack = await _dbContext.TokenPacks.FirstOrDefaultAsync(t => t.Id == tokenId);
			if (tokenPack == null)
				return NotFound("Gem packs with this id not found");

			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);

			if (player == null)
				return NotFound("Player not found");

			player.Token += tokenPack.Value;
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("GetTokens")]
		public async Task<IActionResult> GetToken() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);

			if (player == null)
				return NotFound("Player not found");

			var token = player.Token;
			return Ok(new {token});
		}

		[HttpPost("RemoveToken/{value}")]
		public async Task<IActionResult> RemoveToken(uint value) {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (player == null)
				return NotFound("Player not found");

			if (player.Token < value)
				return BadRequest("player does not have has that amount of token");
			player.Token -= value;
			await _dbContext.SaveChangesAsync();
			return Ok(new {player.Token});
		}

		[HttpPost("RemoveGem/{value}")]
		public async Task<IActionResult> RemoveGem(uint value) {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (player == null)
				return NotFound("Player not found");

			if (player.Gem < value)
				return BadRequest("player does not have has that amount of Gem");
			player.Gem -= value;
			await _dbContext.SaveChangesAsync();
			return Ok(new {player.Gem});
		}

		[HttpPost("MakePlayerOffline")]
		public async Task<IActionResult> MakePlayerOffline() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (!player.IsOnline) return BadRequest("User is already offline");
			player.IsOnline = false;
			_dbContext.Players.Update(player);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("MakePlayerOnline")]
		public async Task<IActionResult> MakePlayerOnline() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (player.IsOnline) return BadRequest("User is already online");
			player.IsOnline = true;
			_dbContext.Players.Update(player);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("FindOpponent")]
		public async Task<ActionResult<PlayerDto>> FindOpponent() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			var opponents = _dbContext.Players.Where(p => p.UserId != UserId && p.Level == player.Level)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Dungeons)
				.ThenInclude(d => d.Slots)
				.ThenInclude(s => s.DefenceItem)
				.Include(p => p.Weapons)
				.Include(p => p.Shields)
				.ToList();
			if (opponents.Count == 0)
				return NotFound("There is no opponent");

			Player opponent = null;

			if (opponents.Count > 1) {
				opponent = opponents.OrderBy(o => Guid.NewGuid()).First();
			} else if (opponents.Count == 1) {
				opponent = opponents.First();
			}

			var dto = _mapper.Map<PlayerDto>(opponent);
			return Ok(dto);
		}
	}
}
