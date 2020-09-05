using System;
using System.Text;
using CyberSoldierServer.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CyberSoldierServer.Helpers {
	public static class Extensions {
		public static IConfiguration GetJwtConfig(this IConfiguration configuration) {
			return configuration.GetSection("JWT");
		}

		public static IdentityBuilder AddJwt(this IdentityBuilder builder, IConfiguration config) {
			builder.Services.Configure<JwtSettings>(config);
			return builder;
		}

		public static AuthenticationBuilder AddJwtBearer(this AuthenticationBuilder builder, IConfiguration config) {
			builder.AddJwtBearer(cfg => {
				cfg.RequireHttpsMetadata = false;
				cfg.SaveToken = true;
				cfg.TokenValidationParameters = new TokenValidationParameters() {
					ValidIssuer = config["Issuer"],
					ValidAudience = config["Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Key"])),
				};
			});
			return builder;
		}

		public static IApplicationBuilder UseAuth(this IApplicationBuilder app) {
			app.UseAuthentication();
			app.UseAuthorization();
			return app;
		}
	}
}
