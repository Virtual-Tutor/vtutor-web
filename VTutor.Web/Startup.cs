using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Identity;
using VTutor.Model;
using VTutor.Web.Server.Services;

namespace AspCoreServer
{
	public class Startup
	{

		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();
			services.AddNodeServices();

			var connectionString = "Server=tcp:dev-vtutor.database.windows.net,1433;Initial Catalog=dev_vtutor;Persist Security Info=False;User ID=isaac;Password=testdatabase1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			services.AddDbContext<VTutorContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddIdentity<ApplicationUser, IdentityRole>()
			  .AddEntityFrameworkStores<VTutorContext>()
			  .AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
		  // Password settings
		  options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 6;

		  // Lockout settings
		  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;

		  // User settings
		  options.User.RequireUniqueEmail = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
		  // Cookie settings
		  options.Cookie.HttpOnly = true;
				options.Cookie.Expiration = TimeSpan.FromDays(150);
				options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
		  options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
		  options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
		  options.SlidingExpiration = true;
			});


			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Angular 4.0 Universal & ASP.NET Core advanced starter-kit web API", Version = "v1" });
			});

			services.AddTransient<IEmailSender, EmailSender>();


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, VTutorContext context)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseStaticFiles();

			app.UseAuthentication();
			//DbInitializer.Initialize(context);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true,
					HotModuleReplacementEndpoint = "/dist/__webpack_hmr"
				});
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				});

				// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.


				app.MapWhen(x => !x.Request.Path.Value.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase), builder =>
				{
					builder.UseMvc(routes =>
			{
					  routes.MapSpaFallbackRoute(
				  name: "spa-fallback",
				  defaults: new { controller = "Home", action = "Index" });
				  });
				});
			}
			else
			{
				app.UseMvc(routes =>
				{
					routes.MapRoute(
			 name: "default",
			 template: "{controller=Home}/{action=Index}/{id?}");

					routes.MapRoute(
			 "Sitemap",
			 "sitemap.xml",
			 new { controller = "Home", action = "SitemapXml" });

					routes.MapSpaFallbackRoute(
			  name: "spa-fallback",
			  defaults: new { controller = "Home", action = "Index" });

				});
				app.UseExceptionHandler("/Home/Error");
			}
		}
	}
}
