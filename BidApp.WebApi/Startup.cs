using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BidApp.DataL;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BidApp.Service.Products;
using BidApp.Service.Hubs;
using BidApp.Service.Rabbit;

namespace BidApp.WebApi
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
            services.AddCors(); // Make sure you call this previous to AddMvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR();
            services.AddMemoryCache();
            var data = Configuration.GetConnectionString("LunchSConnection");
            services.AddDbContext<BidAppContext>(options => {
                options.UseSqlServer(data);
            });
 

            services.AddScoped<IDbContext, BidAppContext>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IBidHub, BidHub>();
            services.AddSingleton<IProducerService, ProducerService > ();
            services.AddSingleton<IConsumerService, ConsumerService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors(
      options => options.AllowAnyOrigin().AllowAnyMethod()
  );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<BidHub>("/bidhub");
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });








            app.UseRabbitListener();
        }



    }
}
