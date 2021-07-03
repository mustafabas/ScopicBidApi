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
using BidApp.Service.Users;
using Swashbuckle.AspNetCore.Swagger;
using BidApp.WebApi.HostedServices;


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
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200");
            }));
            services.AddHostedService<ConsumeRabbitMqService>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CoreSwagger", new Info
                {
                    Title = "BidApp",
                    Version = "1.0.0",
                    Description = "A Simple Bidd App",
                    Contact = new Contact()
                    {
                        Name = "Swagger implementation by Mustafa Bas"

                    },
                    TermsOfService = "http://swagger.io/terms/"
                });

            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR();
            services.AddMemoryCache();
            var data = Configuration.GetConnectionString("LunchSConnection");

            services.AddDbContext<BidAppContext>(
                b => b.UseLazyLoadingProxies()
          .UseSqlServer(data));

            services.AddScoped<IDbContext, BidAppContext>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IProductBidService, ProductBidService>();
            services.AddScoped<BidHub>();


            services.AddSingleton<IProducerService, ProducerService>();
            services.AddSingleton<IConsumerService, ConsumerService>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });


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





            app.UseSwagger()
 .UseSwaggerUI(c =>
 {
     c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "Swagger Test .Net Core");
 });


            app.UseRabbitListener();
        }



    }
}
