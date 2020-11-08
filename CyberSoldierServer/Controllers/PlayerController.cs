using System.Threading.Tasks;
using CyberSoldierServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class PlayerController : AuthApiController {
		private readonly CyberSoldierContext _dbContext;

		public PlayerController(CyberSoldierContext dbContext) {
			_dbContext = dbContext;
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

		[HttpGet("GetGems")]
		public async Task<IActionResult> GetGem() {
			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (player == null)
				return NotFound("Player not found");

			var gem = player.Gem;
			return Ok(new{gem});
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
			return Ok(new{token});
		}
	}
}
