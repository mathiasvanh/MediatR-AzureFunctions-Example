using MediatRExample.Common.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRExample.Modules.Weather
{
    public class Startup : ModuleStartup
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}


