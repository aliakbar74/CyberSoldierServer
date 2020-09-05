using CyberSoldierServer.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CyberSoldierServer.Controllers {
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), ApiController]
	public abstract class AuthApiController : ControllerBase {
		private int _userId;

		protected int UserId {
			get {
				if (_userId == 0)
					_userId = User.GetUserId();
				return _userId;
			}
		}
	}
}
