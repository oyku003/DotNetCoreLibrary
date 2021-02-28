using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidationApp.Web.FluentValidators;
using FluentValidationApp.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web
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
            ///ForDbContext
            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(Configuration["ConStr"]);
            });

            ///ForFluentValidation
            //services.AddSingleton<IValidator<Customer>, CustomerValidator>();//Bu interface ile karşılaşırsan, customervalidatordan nesne örneği al dedik.Aşağıdaki yerine kullanılabilir ama bu sefer de her entity için yapmak gerekir o yüzden alttaki daha mantıklı.
            services.AddControllersWithViews().AddFluentValidation(options=> {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();//proje ayaga kalktıgı zaman tüm ıvalidator interface'ine sahip olan classlara register olarak kaydet dedik.Ayakta olduğu sürece tek bir nesne üzerinden tüm işlmeleri yapacak
            
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });//model state üzerinden kendimiz hata dönmek için ekledik. invalid filterı engellemiş olduk


            ///ForAutomapper
            services.AddAutoMapper(typeof(Startup));//IMapper interfaceini herhangi bir classın constunda kullanırsam ımapper kullanabilirz.
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
