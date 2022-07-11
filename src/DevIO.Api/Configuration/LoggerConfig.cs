using DevIO.Api.Extensions;
using Elmah.Io.Extensions.Logging;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "74e618f973c943f3aa2ecb18b6d9f699";
                o.LogId = new Guid("a3cc30fb-0798-4b30-b8e6-6d927645ffed");
            });

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "74e618f973c943f3aa2ecb18b6d9f699";
                    options.LogId = new Guid("a3cc30fb-0798-4b30-b8e6-6d927645ffed");
                    options.HeartbeatId = "API Fornecedores";

                })
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI()
                .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
