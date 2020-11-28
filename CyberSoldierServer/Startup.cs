using System;
using System.Collections.Generic;
using CyberSoldierServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using CyberSoldierServer.Dtos;
using CyberSoldierServer.Helpers.Extensions;
using CyberSoldierServer.Models.Auth;
using CyberSoldierServer.Services;
using EmailService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace CyberSoldierServer {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();

			services.AddDbContext<CyberSoldierContext>(opt =>
				{ opt.UseNpgsql(Configuration.GetConnectionString("CyberSoldierConnection")); });

			services.AddIdentity<AppUser, Role>(opt => {
					opt.Password.RequiredLength = 6;
					opt.Password.RequireDigit = false;
					opt.Password.RequireNonAlphanumeric = false;
					opt.Password.RequireNonAlphanumeric = false;
					// opt.User.RequireUniqueEmail = true;
				})
				.AddEntityFrameworkStores<CyberSoldierContext>()
				.AddDefaultTokenProviders()
				.AddJwt(Configuration.GetJwtConfig());


			services.AddAuthentication()
				.AddJwtBearer(Configuration.GetJwtConfig());
			services.AddAuthorization();


			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo {
					Version = "v1",
					Title = "MyAPI"
				});
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
					Description = "JWT containing userid claim",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
				});
				var security =
					new OpenApiSecurityRequirement {
						{
							new OpenApiSecurityScheme {
								Reference = new OpenApiReference {
									Id = "Bearer",
									Type = ReferenceType.SecurityScheme
								},
								UnresolvedReference = true
							},
							new List<string>()
						}
					};
				c.AddSecurityRequirement(security);
			});

			services.AddMvc().AddFluentValidation();
			services.AddTransient<IValidator<UserSignUpDto>, UserSignValidator>();

			services.AddSuperUser(Configuration.GetSuperUserConfig());
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddScoped<IConvertErrorToCodeService, ConvertErrorToCodeService>();
			services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
			services.AddScoped<IEmailSender, EmailSender>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CyberSoldierContext context) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			context.Database.Migrate();

			// app.UseHttpsRedirection();

			app.UseRouting();
			app.UseAuth();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

			app.UseSwagger();
			app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI"); });
		}
	}
}
