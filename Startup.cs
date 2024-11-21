using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using UserApiTest.Models;
using UserApiTest.Repository;

namespace UserApiTest
{
    /// <summary>
    /// Configures the application's services and HTTP request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the database context with a SQLite provider.
            services.AddDbContext<UserContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection") 
                                  ?? "Data Source=Users.db"));

            // Register the user repository.
            services.AddScoped<IUserRepository, UserRepository>();

            // Add controllers.
            services.AddControllers();

            // Add Swagger for API documentation.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User API Test",
                    Version = "v1",
                    Description = "API for managing user data."
                });
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable developer exception page in development environment.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable Swagger in development for easier testing.
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API Test v1");
                    c.RoutePrefix = string.Empty; // Serve Swagger UI at the root.
                });
            }

            // Redirect HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            // Configure routing.
            app.UseRouting();

            // Configure authorization (if required).
            app.UseAuthorization();

            // Map endpoints to controllers.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
