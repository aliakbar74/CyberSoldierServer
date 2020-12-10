using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.EjectDtos;
using CyberSoldierServer.Dtos.InsertDtos;
using CyberSoldierServer.Models.Auth;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class PlayerController : AuthApiController {
		private readonly CyberSoldierContext _dbContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public PlayerController(CyberSoldierContext dbContext, IMapper mapper, UserManager<AppUser> userManager) {
			_dbContext = dbContext;
			_mapper = mapper;
			_userManager = userManager;
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
				.Include(p => p.User)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Dungeons)
				.ThenInclude(d => d.Slots)
				.ThenInclude(s => s.DefenceItem)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Server)
				.Include(p => p.Camp)
				.ThenInclude(c => c.Cpus)
				.ThenInclude(c => c.Cpu)
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

		[HttpPost("AddAttacker")]
		public async Task<IActionResult> AddAttacker([FromBody] AttackerInsertDto Dto) {
			var victim = await _dbContext.Players.FirstOrDefaultAsync(p => p.Id == Dto.VictimId);
			if (victim == null) {
				return NotFound("Victim not found");
			}

			var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == UserId);
			var attacker = new Attacker {
				PlayerId = victim.Id,
				AttackerPlayerId = player.Id,
				CpuId = Dto.CpuId,
				DungeonCount = Dto.DungeonCount
			};

			await _dbContext.Attackers.AddAsync(attacker);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("GetAttackers")]
		public async Task<ActionResult<List<AttackerDto>>> GetAttackers() {
			var player = await _dbContext.Players.Where(p => p.UserId == UserId)
				.Include(p=>p.Attackers)
				.ThenInclude(a=>a.Cpu)
				.FirstOrDefaultAsync();

			if (player.Attackers.Count == 0)
				return NotFound("There is no attacker");

			var dtos = new List<AttackerDto>(player.Attackers.Count);

			foreach (var attacker in player.Attackers) {
				var ap = await _dbContext.Players.Where(p => p.Id == attacker.AttackerPlayerId)
					.Include(p=>p.User)
					.FirstOrDefaultAsync();

				var ad = new AttackerDto {
					Cpu = _mapper.Map<CampCpuDto>(attacker.Cpu),
					AttackerId = ap.Id,
					Level = ap.Level,
					UserName = ap.User.UserName,
					DungeonCount = attacker.DungeonCount
				};
				dtos.Add(ad);
			}

			return Ok(dtos);
		}

		[HttpPost("Attack/{id}")]
		public async Task<ActionResult<PlayerDto>> Attack(int id) {
			var victim = await _dbContext.Players.FirstOrDefaultAsync(p => p.Id == id);
			if (victim == null)
				return NotFound("Player not found");
			var dto = _mapper.Map<PlayerDto>(victim);
			return Ok(dto);
		}
	}
}
