using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MusicManger.Server.Core.Infrastructure;
using MusicManager.Server.Core.Repository;
using MusicManager.Server.Core.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MusicManager.Server.Core.Services;
using MusicManager.Server.Core.Model;
using Microsoft.AspNetCore.Identity;
using System;

namespace MusicManager.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));

            services.AddDbContext<MusicManagerContext>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPasswordResetLinkRepository, PasswordResetLinkRepository>();

            // Services
            services.AddScoped<IRequestDataService, RequestDataService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordResetLinkService, PasswordResetLinkService>();
            services.AddScoped<IFileService, FileService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x     // ADJUST THIS IN PRODUCTION RETARD
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
