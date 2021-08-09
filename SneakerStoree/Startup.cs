using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SneakerStoree.DBContexts;
using SneakerStoree.Entities;
using SneakerStoree.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvc(c => c.EnableEndpointRouting = false);
            services.AddDbContextPool<SneakerStoreDBContex>(option => option.UseSqlServer(configuration.GetConnectionString("SneakerStoreConecting")));
            services.AddIdentity<AppIdentityUser, IdentityRole>().AddEntityFrameworkStores<SneakerStoreDBContex>();
            services.AddScoped<ITradeMarkService, TradeMarkService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISneakerService, SneakerService>();

            services.AddDistributedMemoryCache();
            services.AddSession(cfg =>
            {
                cfg.Cookie.Name = "sneakerstore";
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler("/Errors/");
            app.UseStatusCodePagesWithRedirects("/Errors/NotFound");
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
