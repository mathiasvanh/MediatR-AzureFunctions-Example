using System.IO;
using FluentValidation.AspNetCore;
using MediatR;
using MediatRExample.Common;
using MediatRExample.Common.Mapping;
using MediatRExample.Common.Modules;
using MediatRExample.Common.Queries;
using MediatRExample.Common.Validation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Startup = MediatRExample.Host.Startup;

[assembly: FunctionsStartup(typeof(Startup))]
namespace MediatRExample.Host;
public class Startup : FunctionsStartup
{
     private IConfigurationRoot Configuration { get; set; } = null!;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var environment = builder.GetContext().EnvironmentName;

            builder.Services.AddMediatR(typeof(Startup), typeof(IQuery<>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddModules(Configuration);
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddOptions<GlobalOptions>()
                .Configure<IConfiguration>((settings, config) =>
                {
                    Configuration.GetSection(GlobalOptions.SectionName).Bind(settings);
                });

            AppDependencyResolver.Init(builder.Services.BuildServiceProvider());
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var environment = builder.GetContext().EnvironmentName;

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(builder.GetContext().ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(builder.GetContext().ApplicationRootPath, $"appsettings.{environment}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

            Configuration = builder.ConfigurationBuilder.Build();
        }
}