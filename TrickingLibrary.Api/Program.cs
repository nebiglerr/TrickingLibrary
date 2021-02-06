using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrickingLibrary.Data;
using TrickingLibrary.Models;
using TrickingLibrary.Models.Moderation;

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
                    ctx.Add(new Difficulty
                    {
                        Id = "medium",
                        Name = "Medium",
                        Description = "medium hard"
                    });
                    ctx.Difficulties.Add(new Difficulty
                    {
                        Id = "hard",
                        Name = "Hard",
                        Description = "hard hard"
                    });
                    ctx.Categories.Add(new Category
                    {
                        Id = "kick",
                        Name = "Kick",
                        Description = "kick kick"
                    });
                    ctx.Categories.Add(new Category
                    {
                        Id = "flip",
                        Name = "Flip",
                        Description = "flip flip"
                    });
                    ctx.Categories.Add(new Category
                    {
                        Id = "translation",
                        Name = "Translation",
                        Description = "translation"
                    });
                    ctx.Add(new Trick
                    {
                        Id = "forwards-roll",
                        Name = "Forwards Roll",
                        Difficulty = "easy",
                        Description = "This is a test Forwards roll ",
                        TrickCategories = new List<TrickCategory>
                        {
                            new TrickCategory
                            {
                                CategoryId = "flip",
                            }
                        }
                    });
                    ctx.Add(new Trick
                    {
                        Id = "backwards-roll",
                        Name = "Backwards Roll",
                        Difficulty = "easy",
                        Description = "This is a test backwards roll ",
                        TrickCategories = new List<TrickCategory>
                        {
                            new TrickCategory
                            {
                                CategoryId = "flip",
                            }
                        }
                    });
                    ctx.Add(new Trick
                    {
                        Id = "back-flip",
                        Name = "Back Flip",
                        Description = "This is a test back flip ",
                        Difficulty = "medium",
                        TrickCategories = new List<TrickCategory>
                        {
                            new TrickCategory
                            {
                                CategoryId = "flip"
                            }
                        },
                        Prerequisites = new List<TrickRelationship>
                        {
                            new TrickRelationship
                            {
                                PrerequisiteId = "backwards-roll"
                            }
                        }
                    });
                    ctx.Add(new ModerationItem
                    {
                        Target = "forwards-roll",
                        Type = ModerationTypes.Trick,
                    });
                    /*ctx.Add(new Submission
                    {
                        TrickId = "back-flip",
                        Description = "Test description ı have tired to go max height",
                        Video = "test2.mp4",
                        VideoProcessed = true
                    });   
                    ctx.Add(new Submission
                    {
                        TrickId = "back-flip",
                        Description = "Test description ı have tired to go max height",
                        Video = "video4.mp4",
                        VideoProcessed = true
                    });*/
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