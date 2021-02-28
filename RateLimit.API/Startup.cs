using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimit.API
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
            //Rate limit için..
            services.AddOptions();//appsettingjson içierisindeki key valueları class içinde okuyabiliriz. O yüzden ekledik.
            services.AddMemoryCache();//Cache için ekledik. Memoryde yani ramde tutucaz request sayılarını o yüzden ekledik.
            //services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));//ip değerlerini appsettingden okuyabilmek için gidip ipleri alabilmesi için ekledik.
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));//Client değerlerini appsettingden okuyabilmek için 
            //services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));//policy yani şartname oluşturucaz. Mesela şurdan istek olursa 100 req , şurda olursa bin requ yap demek için
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));//policy yani şartname oluşturucaz. Mesela şurdan istek olursa 100 req , şurda olursa bin requ yap demek için
            //services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();//Okunacak memery cache için, tek inctanse için singleton uygun. Birden fazla instance olsaydı örneğin docker kullandık bu sefer distrubute cache kullanmamız gerekirdi mesela redis cache gibi. Eger bunu yapmazsak sayılar birbirine karışır. O zaman burada MemoryCacheIpPolicyStore yerine distrubute... olanı belirtmemiz gerekirdi.
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();//Okunacak memory cache için, tek inctanse için singleton uygun. Birden fazla instance olsaydı örneğin docker kullandık bu sefer distrubute cache kullanmamız gerekirdi mesela redis cache gibi. Eger bunu yapmazsak sayılar birbirine karışır. O zaman burada MemoryCacheIpPolicyStore yerine distrubute... olanı belirtmemiz gerekirdi.
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//ilk olarak belirttiğimiz middleware e gelineceği için ve request içindeki dataları okuyabilmemmiz için
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseIpRateLimiting();//ConfigureServices içine belirttiğimiz kurallara göre uygulama sağlayacak.
            app.UseClientRateLimiting();//ConfigureServices içine belirttiğimiz kurallara göre uygulama sağlayacak.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
