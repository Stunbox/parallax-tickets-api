
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using parallax_tickets_api.DAL;
using System;
using System.Text;

namespace parallax_tickets_api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Configuration.AddJsonFile("appsettings.json");
			var secretKey = builder.Configuration.GetSection("Jwt").GetSection("Key").ToString();
			var keyBytes = Encoding.UTF8.GetBytes(secretKey);

			builder.Services.AddAuthentication(config =>
			{
				config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = true;
				options.SaveToken = true;
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
				};
			});

			// Conexion a postgress
			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			//Timestamps
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
