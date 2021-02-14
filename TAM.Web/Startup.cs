using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Classes;
using TAM.Service.Interfaces;

namespace TAM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<KorisnickiRacun, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddScoped(typeof(IRepository<>), typeof(Repository.Repository<>));
            services.AddTransient<ISvrhaUplateService, SvrhaUplateService>();
            services.AddTransient<ITipPolaznikaService, TipPolaznikaService>();
            services.AddTransient<IProstorijaService, ProstorijaService>();
            services.AddTransient<ITipDogadjajaService, TipDogadjajaService>();
            services.AddTransient<IKategorijaKursaService, KategorijaKursaService>();
            services.AddTransient<IKategorijaObavijestiService, KategorijaObavijestiService>();
            services.AddTransient<IExceptionHandlerService, ExceptionHandlerService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IKursService, KursService>();
            services.AddTransient<IPortirService, PortirService>();
            services.AddTransient<IPredavacService, PredavacService>();
            services.AddTransient<IObavijestService, ObavijestService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
