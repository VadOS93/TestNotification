using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Notifications.BLL.Extensions.DI;
using Notifications.DAL.Contexts;

using TestNotification.Handlers;

namespace TestNotification;

public class Program
{
	public static void Main(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			   .SetBasePath(Directory.GetCurrentDirectory())
			   .AddJsonFile("appsettings.json")
			   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
			   .AddEnvironmentVariables()
			   .Build();
		var builder = WebApplication.CreateBuilder(args);

		// some config
		builder.Configuration.Sources.Clear();
		builder.Configuration.AddConfiguration(configuration);

		// Add services to the container.
		var connStr = builder.Configuration.GetConnectionString("Default");
		builder.Services.AddDbContext<NotificationContext>(options => options.UseSqlServer(connStr, x => x.MigrationsHistoryTable("__NotificationMigrationsHistory")));

		builder.Services.AddControllers();
		builder.Services.ActivateBasicServices();

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		// Add authentication services
		builder.Services.AddAuthentication("BasicAuthentication")
			.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

		builder.Services.AddAuthorization();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

			// Include security definitions for Swagger
			c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				Scheme = "basic",
				In = ParameterLocation.Header,
				Description = "Basic Authorization header using the Bearer scheme.",
			});

			// Include security requirements for Swagger
			c.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "basic"
					}
				},
				new string[] {}
			}
		});
		});

		var app = builder.Build();

		InitializeDatabase(app);
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

	private static void InitializeDatabase(IApplicationBuilder app)
	{
		using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
		{
			try
			{
				var pers = serviceScope.ServiceProvider.GetRequiredService<NotificationContext>();
				if (!pers.Database.EnsureCreated())
					pers.Database.Migrate();
			}
			catch
			{

			}
		}
	}
}