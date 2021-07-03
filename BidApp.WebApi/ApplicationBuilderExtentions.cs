using BidApp.Service.Rabbit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace BidApp.WebApi
{
    public static class ApplicationBuilderExtentions
    {
        public static IConsumerService Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<IConsumerService>();

            var life = app.ApplicationServices.GetService<IApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);


            return app;
        }

        private static void OnStarted()
        {
            Listener.ReceiveMessageFromQ();
        }
    }
}
