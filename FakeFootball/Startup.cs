using FakeFootball.Data;
using FakeFootball.Data.JwtData;
using FakeFootball.Hangfire;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;

namespace FakeFootball
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((opts) =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = Configuration.GetSection("Jwt:Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:Key").Value))
                };
            });

            services.AddDbContext<JwtUserDbContext>(c => c.UseNpgsql(Configuration.GetConnectionString("JwtUsersDB")));

            services.AddDbContext<SoccerDbContext>(c =>
                c.UseNpgsql(Configuration.GetConnectionString("SoccerDB"), b => b.MigrationsAssembly("FakeFootball")));

            services.AddControllers().AddNewtonsoftJson(cfg =>
            {
                cfg.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHangfire(cfg => cfg.UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireStorage")));

            services.AddScoped<ITeamRepo, TeamRepo>();
            services.AddScoped<IScoresRepo, ScoresRepo>();
            services.AddScoped<IHangfireJobs, HangfireJobs>();
            services.AddScoped<IJwtUserRepo, JwtUserRepo>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHangfireJobs hangfireJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseHangfireDashboard("/hangfire");
            }

            app.UseHangfireServer();

            app.UseAuthentication();
            app.UseAuthorization();

            hangfireJobs.UpdateScores();
            hangfireJobs.UpdateKey();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
