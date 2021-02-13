using System.Security.Claims;
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
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
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
                config.LogoutPath = "/api/auth/logout";
            });
            var identityIdentityServer= services.AddIdentityServer();
            
            identityIdentityServer.AddAspNetIdentity<IdentityUser>(); 

            if (_env.IsDevelopment())
            {
                identityIdentityServer.AddInMemoryIdentityResources(new IdentityResource[]
                {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile(),
                 new IdentityResource(TrickingLibraryConstants.IdentityResource.RoleScope,new []{TrickingLibraryConstants.Claims.Role})
                });
                
                identityIdentityServer.AddInMemoryApiScopes(new ApiScope[]
                {
                    new ApiScope(IdentityServerConstants.LocalApi.ScopeName, new []{TrickingLibraryConstants.Claims.Role}),
                });
                identityIdentityServer.AddInMemoryClients(new Client[]
                {
                    new Client
                    {
                        ClientId = "web-client",
                        AllowedGrantTypes = GrantTypes.Code,
                        RedirectUris = new[] {"https://localhost:3000/oidc/sign-in-callback.html"},
                        PostLogoutRedirectUris = new[] {"https://localhost:3000"},
                        AllowedCorsOrigins = new[] {"https://localhost:3000"},
                        AllowedScopes = new []
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.LocalApi.ScopeName,
                            TrickingLibraryConstants.IdentityResource.RoleScope
                        },
                        RequirePkce = true,
                        AllowAccessTokensViaBrowser = true,
                        RequireConsent = false,
                        RequireClientSecret = false,
                        
                        
                    }
                });
                identityIdentityServer.AddDeveloperSigningCredential();
            }

            services.AddLocalApiAuthentication();
            services.AddAuthorization(options =>
            {
               
                options.AddPolicy(TrickingLibraryConstants.Policies.Mod ,policy =>
                {
                    var is4Policy = options.GetPolicy(IdentityServerConstants.LocalApi.PolicyName);
                    policy.Combine(is4Policy);
                    policy.RequireClaim(TrickingLibraryConstants.Claims.Role,TrickingLibraryConstants.Roles.Mod);
                });
            });

        }
    }

    public struct TrickingLibraryConstants
    {
        public struct Policies
        {
            public const string Mod = nameof(Mod);
        } 
        public struct Claims
        {
            public const string Role = "role";
        }  
        public struct IdentityResource
        {
            public const string RoleScope = "role";
        }
        public struct Roles
        {
            public const string Mod = nameof(Mod);
        }
    }
}