namespace OrderService
{
    public class Serviceconfiguration
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>
               (p => new ConsulClient(consulConfig =>
               {

                   consulConfig.Address = new Uri("http://localhost:8500");

               }));
            return services;

        }
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app,
            IConfiguration configurationSetting)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<AspNetCore.Hosting.IApplicationLifetime>();

            var registration = new AgentServiceRegistration()
            {

                ID = "order",
                Name = "Order",
                Address = "localhost",
                Port = 5140
            };
            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);
            lifetime.ApplicationStopping.Register(() =>

            {
                logger.LogInformation("UnRegistreing from Consul");
            });
            return app;
        }

    }
}
