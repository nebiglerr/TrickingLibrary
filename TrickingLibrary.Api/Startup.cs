using System.Threading.Channels;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrickingLibrary.BackgroundServices;
using TrickingLibrary.BackgroundServices.VideoEditing;
using TrickingLibrary.Data;

namespace TrickingLibrary
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private const string AllCors = "All";

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: BU KISMI ÖĞRENMEK ZORUNDASIN ConfigureServices
     
          
            services.AddDbContext<AppDbContext>(optios => optios.UseInMemoryDatabase("Dev"));
            AddIdentity(services);
            services.AddControllers();
            
            services.AddRazorPages();
            
            services.AddHostedService<VideoEditingBackgroundService>()
                .AddSingleton((_) => Channel.CreateUnbounded<EditVideoMessage>())
                .AddSingleton<VideoManager>()
                .AddCors(options =>
                options.AddPolicy(AllCors,build => 
                    build.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
        }

        public void Configure(IApplicationBuilder app) 
        {
            //TODO: BU KISMI ÖĞRENMEK ZORUNDASIN Configure
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseCors(AllCors);
            app.UseRouting();
            app.UseAuthentication();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>(config => config.UseInMemoryDatabase("DevIdentity"));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                if (_env.IsDevelopment())
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                }
                else
                {
                    
                }

            }).AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/Login";
            });
            var identityIdentityServer= services.AddIdentityServer();
            
            identityIdentityServer.AddAspNetIdentity<IdentityUser>(); 

            if (_env.IsDevelopment())
            {
                identityIdentityServer.AddInMemoryIdentityResources(new IdentityResource[]
                {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile()
                });
                identityIdentityServer.AddInMemoryClients(new Client[]
                {
                    new Client
                    {
                        ClientId = "web-client",
                        AllowedGrantTypes = GrantTypes.Code,
                        RedirectUris = new[] {"http://localhost:3000"},
                        PostLogoutRedirectUris = new[] {"http://localhost:3000"},
                        AllowedCorsOrigins = new[] {"http://localhost:3000"},
                        AllowedScopes = new []
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile
                        },
                        RequirePkce = true,
                        AllowAccessTokensViaBrowser = true,
                        RequireConsent = false,
                        RequireClientSecret = false,
                        
                        
                    }
                });
                identityIdentityServer.AddDeveloperSigningCredential();
            }
        }
    }
}