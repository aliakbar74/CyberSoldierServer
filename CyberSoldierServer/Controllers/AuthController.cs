using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CyberSoldierServer.Data;
using CyberSoldierServer.Dtos;
using CyberSoldierServer.Models;
using CyberSoldierServer.Models.Auth;
using CyberSoldierServer.Models.PlayerModels;
using CyberSoldierServer.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase {
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly IConfiguration _configuration;
		private readonly JwtSettings _jwtSettings;
		private readonly SuperUser _superUser;
		private readonly CyberSoldierContext _dbContext;

		public AuthController(IMapper mapper, UserManager<AppUser> userManager, RoleManager<Role> roleManager,
			IConfiguration configuration, IOptions<JwtSettings> jwtSettings, CyberSoldierContext dbContext, IOptions<SuperUser> superUser) {
			_mapper = mapper;
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
			_dbContext = dbContext;
			_superUser = superUser.Value;
			_jwtSettings = jwtSettings.Value;
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUp(UserSignUpDto userSignUpDto) {
			var user = _mapper.Map<UserSignUpDto, AppUser>(userSignUpDto);
			var userCreateResult = await _userManager.CreateAsync(user, userSignUpDto.Password);

			if (userCreateResult.Succeeded) {
				var player = new Player {
					UserId = user.Id
				};
				await _dbContext.Players.AddAsync(player);
				await _dbContext.SaveChangesAsync();
				return Ok();
			}
			return Problem(userCreateResult.Errors.First().Description, null, 500);
		}

		[HttpPost("SignIn")]
		public async Task<IActionResult> SignIn(UserSignUpDto userDto) {
			var user = _userManager.Users.SingleOrDefault(u => u.UserName == userDto.UserName);

			if (user is null) {
				return NotFound("User not found!");
			}

			var signInResult = await _userManager.CheckPasswordAsync(user, userDto.Password);

			if (signInResult) {
				return Ok(new {token = GenerateJwt(user)});
			}

			return BadRequest("User name or pass is incorrect!");
		}

		[HttpPost("CreateRoles")]
		public async Task<IActionResult> CreateRoles() {
			var superUser = new AppUser {
				// UserName = _configuration.GetValue<string>("SuperUser:UserName")
				UserName = _superUser.UserName
			};

			if (!await _roleManager.RoleExistsAsync("Admin")) {
				var role = new Role {Name = "Admin"};
				await _roleManager.CreateAsync(role);
			}

			// var pass = _configuration.GetValue<string>("SuperUser:Password");
			var pass = _superUser.Password;
			var user = await _userManager.FindByNameAsync(superUser.UserName);

			if (user == null) {
				var creatResult = await _userManager.CreateAsync(superUser, pass);

				if (creatResult.Succeeded)
					await _userManager.AddToRoleAsync(superUser, "Admin");
			}

			return Ok();
		}

		private string GenerateJwt(AppUser user) {
			var claims = new List<Claim> {
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
				new Claim("role", "Admin")
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddMinutes(_jwtSettings.Expire);

			var token = new JwtSecurityToken(
				_jwtSettings.Issuer,
				_jwtSettings.Issuer,
				claims,
				expires: expires,
				signingCredentials: cred);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
