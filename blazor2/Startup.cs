using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// using blazor2.Data;

namespace blazor2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // thêm signalR
            services.AddSignalR();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // DI httpclient
            services.AddHttpClient();

            // custom httpclient
            services.AddHttpClient("ShoeShopApi", client =>
            {
                client.BaseAddress = new Uri("https://apistore.cybersoft.edu.vn/");
            });

            // Đăng ký dịch vụ CounterService với vòng đời
            // Singleton: Dịch vụ sẽ được tạo một lần và sử dụng chung trong toàn bộ ứng dụng
            services.AddSingleton<CounterService>();
            services.AddSingleton<BurgerService>();
            services.AddSingleton<CryptoService>();
            services.AddSingleton<ShoeShopStateService>();

            // đăng ký dịch vụ RoomChatService
            services.AddSingleton<RoomChatService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // tự động chuyển về https
            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();

                // cho biết sẽ map với hub nào
                // vì đang để hub chung source nên "/roomHub" => url hiện tại/roomHub
                // nếu hub ở 1 server khác thì truyền 1 url chính xác vào
                endpoints.MapHub<RoomHub>("/roomHub");
                endpoints.MapHub<ShoeShopHub>("/shoeShopHub");

                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
