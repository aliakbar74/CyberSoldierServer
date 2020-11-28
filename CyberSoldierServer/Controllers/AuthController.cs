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
using CyberSoldierServer.Models.Auth;
using CyberSoldierServer.Models.PlayerModels;
using CyberSoldierServer.Services;
using CyberSoldierServer.Settings;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CyberSoldierServer.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : AuthApiController {
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly IConfiguration _configuration;
		private readonly JwtSettings _jwtSettings;
		private readonly SuperUser _superUser;
		private readonly CyberSoldierContext _dbContext;
		private readonly IConvertErrorToCodeService _convertErrorToCode;
		private readonly IEmailSender _emailSender;

		public AuthController(IMapper mapper, UserManager<AppUser> userManager, RoleManager<Role> roleManager,
			IConfiguration configuration, IOptions<JwtSettings> jwtSettings, CyberSoldierContext dbContext,
			IOptions<SuperUser> superUser, IConvertErrorToCodeService convertErrorToCode, IEmailSender emailSender) {
			_mapper = mapper;
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
			_dbContext = dbContext;
			_convertErrorToCode = convertErrorToCode;
			_emailSender = emailSender;
			_superUser = superUser.Value;
			_jwtSettings = jwtSettings.Value;
		}

		[AllowAnonymous]
		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUp(UserSignUpDto userSignUpDto) {
			var user = _mapper.Map<UserSignUpDto, AppUser>(userSignUpDto);

			var message = new Message(new[] {userSignUpDto.Email}, "Test email", "this is test email");
			await _emailSender.SendEmail(message);

			if (!string.IsNullOrEmpty(userSignUpDto.Email)) {
				if (!ModelState.IsValid) {
					return BadRequest();
				}

				if (await _userManager.FindByEmailAsync(user.Email) != null) {
					var error = _userManager.ErrorDescriber.DuplicateEmail(user.Email);
					return Problem($"{_convertErrorToCode.ConvertErrorToCode(error.Code).ToString()} : {error.Description}", null, 500);
				}
			}

			var userCreateResult = await _userManager.CreateAsync(user, userSignUpDto.Password);
			if (userCreateResult.Succeeded) {
				var player = new Player {
					UserId = user.Id
				};
				player.IsOnline = true;
				await _dbContext.Players.AddAsync(player);
				await _dbContext.SaveChangesAsync();

				return Ok();
			}

			var code = _convertErrorToCode.ConvertErrorToCode(userCreateResult.Errors.First().Code);
			return Problem($"{code.ToString()} : {userCreateResult.Errors.First().Description}", null, 500);
		}

		[AllowAnonymous]
		[HttpPost("SignIn")]
		public async Task<IActionResult> SignIn(UserSignUpDto userDto) {
			var user = _userManager.Users.SingleOrDefault(u => u.UserName == userDto.UserName);

			if (user is null) {
				return NotFound("User not found!");
			}

			var signInResult = await _userManager.CheckPasswordAsync(user, userDto.Password);

			if (signInResult) {
				var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == user.Id);
				player.IsOnline = true;
				_dbContext.Players.Update(player);
				await _dbContext.SaveChangesAsync();
				return Ok(new {token = GenerateJwt(user)});
			}

			return BadRequest("User name or pass is incorrect!");
		}

		[HttpPost("ChangeUserName/{newUserName}")]
		public async Task<IActionResult> ChangeUserName(string newUserName) {
			var user = await _userManager.FindByIdAsync(UserId.ToString());
			var result = await _userManager.SetUserNameAsync(user, newUserName);
			if (!result.Succeeded) {
				var code = _convertErrorToCode.ConvertErrorToCode(result.Errors.First().Code);
				return Problem($"{code.ToString()} : {result.Errors.First().Description}", null, 500);
			}

			return Ok();
		}

		[HttpPost("ChangePassword/{newPassword}")]
		public async Task<IActionResult> ChangePassword(string newPassword) {
			var user = await _userManager.FindByIdAsync(UserId.ToString());
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
			if (!result.Succeeded) {
				var code = _convertErrorToCode.ConvertErrorToCode(result.Errors.First().Code);
				return Problem($"{code.ToString()} : {result.Errors.First().Description}", null, 500);
			}

			return Ok();
		}

		[HttpPost("ChangeEmail/{newEmail}")]
		public async Task<IActionResult> ChangeEmail(string newEmail) {
			var user = await _userManager.FindByIdAsync(UserId.ToString());
			var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
			var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
			if (!result.Succeeded) {
				var code = _convertErrorToCode.ConvertErrorToCode(result.Errors.First().Code);
				return Problem($"{code.ToString()} : {result.Errors.First().Description}", null, 500);
			}

			return Ok();
		}

		[HttpPost("CreateRoles")]
		public async Task<IActionResult> CreateRoles() {
			var superUser = new AppUser {
				UserName = _superUser.UserName
			};

			if (!await _roleManager.RoleExistsAsync("Admin")) {
				var role = new Role {Name = "Admin"};
				await _roleManager.CreateAsync(role);
			}

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
