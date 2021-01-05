using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    ctx.Difficulties.Add(new Difficulty
                    {
                        Id = "easy",
                        Name = "Easy",
                        Description = "easy test"
                    });
                    ctx.Difficulties.Add(new Difficulty
                    {
                        Id = "hard",
                        Name = "hard",
                        Description = "hard hard"
                    });
                    ctx.Categories.Add(new Category
                    {
                        Id = "kick",
                        Name = "kick",
                        Description = "kick kick"
                    });
                    ctx.Categories.Add(new Category
                    {
                        Id = "flip",
                        Name = "flip",
                        Description = "flip flip"
                    });
                    ctx.Categories.Add(new Category
                    {
                        Id = "translation",
                        Name = "translation",
                        Description = "translation"
                    });
                    ctx.SaveChanges();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}