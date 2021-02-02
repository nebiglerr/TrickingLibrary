using System.Threading.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrickingLibrary.BackgroundServices;
using TrickingLibrary.Data;

namespace TrickingLibrary
{
    public class Startup
    {
        private const string AllCors = "All";
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: BU KISMI ÖĞRENMEK ZORUNDASIN ConfigureServices
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // if don't set default value is: 128 MB
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });
            services.AddControllers();
            services.AddDbContext<AppDbContext>(optios => optios.UseInMemoryDatabase("Dev"));
            services.AddHostedService<VideoEditingBackgroundService>();
            services.AddSingleton<Channel<EditVideoMessage>>((_) => Channel.CreateUnbounded<EditVideoMessage>());
            services.AddCors(options =>
                options.AddPolicy(AllCors,build => build.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            //TODO: BU KISMI ÖĞRENMEK ZORUNDASIN Configure
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseCors(AllCors);
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}