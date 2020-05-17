using EstateAgent.Business.Abstract;
using EstateAgent.Business.Concrete;
using EstateAgent.Cache;
using EstateAgent.Cache.Redis.Abstract;
using EstateAgent.Cache.Redis.Concrete;
using EstateAgent.Dal.Abstract;
using EstateAgent.Dal.Concrete.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EstateAgent.MvcUI
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
           
            services.AddScoped<IResidentialService, ResidentialManager>();
            services.AddScoped<IResidentialDal, ResidentialDal>();
            services.AddScoped<IRedisCacheService, RedisCacheManager>();
            services.AddScoped<IResidentialRedisCacheService, ResidentialRedisCacheManager>();

            //Binding "MongoDbSettings" which is in appsettings.json to MongoDbSetting model
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb_EstateAgent"));
            services.AddSingleton<IMongoDbSettings>(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddControllersWithViews();
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
                    pattern: "{controller=Residential}/{action=Index}/{id?}");
            });
        }
    }
}
