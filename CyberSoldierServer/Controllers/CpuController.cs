﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos.InsertDtos;
using CyberSoldierServer.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	public class CpuController : AuthApiController {
		private readonly IMapper _mapper;
		private readonly CyberSoldierContext _dbContext;

		public CpuController(IMapper mapper, CyberSoldierContext dbContext) {
			_mapper = mapper;
			_dbContext = dbContext;
		}

		[HttpPost]
		public async Task<IActionResult> AddCpu([FromBody] ServerCpuInsertDto model) {
			var player = await _dbContext.Players.Where(p=>p.UserId==UserId)
				.Include(p=>p.Camp)
				.ThenInclude(p => p.Server)
				.Include(p=>p.Camp)
				.ThenInclude(p=>p.Cpus)
				.FirstOrDefaultAsync();

			if (player == null)
				return NotFound("Path not found");
			if (player.Camp.Server.CpuCount <= player.Camp.Cpus.Count)
				return BadRequest($"You already have {player.Camp.Server.CpuCount} cpu");

			var cpu = _mapper.Map<CampCpu>(model);
			cpu.CampId = player.Camp.Id;

			await _dbContext.ServerCpus.AddAsync(cpu);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> UpgradeCpu(int id) {
			var serverCpu = await _dbContext.ServerCpus.Where(c => c.Id == id)
				.Include(c => c.Cpu)
				.FirstOrDefaultAsync();

			if (serverCpu == null)
				return NotFound("Cpu not found");

			var cpu = await _dbContext.Cpus.Where(c => c.CpuType == serverCpu.Cpu.CpuType && c.Level == serverCpu.Cpu.Level + 1)
				.FirstOrDefaultAsync();
			if (cpu == null)
				return NotFound("Cpu is at max level");

			serverCpu.Cpu = cpu;

			_dbContext.ServerCpus.Update(serverCpu);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("RemoveCpu")]
		public async Task<IActionResult> RemoveCpu(int id) {
			var cpu = await _dbContext.ServerCpus.FirstOrDefaultAsync(c => c.Id == id);
			if (cpu == null) return NotFound("Cpu not found");
			_dbContext.ServerCpus.Remove(cpu);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
