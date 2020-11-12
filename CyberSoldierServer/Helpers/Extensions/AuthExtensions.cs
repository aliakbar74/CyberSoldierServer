using System.Security.Claims;

namespace CyberSoldierServer.Helpers.Extensions {
	public static class AuthExtensions {
		public static int GetUserId(this ClaimsPrincipal principal) {
			return int.Parse((principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") ?? principal.FindFirst("sub")).Value);
		}
	}
}
