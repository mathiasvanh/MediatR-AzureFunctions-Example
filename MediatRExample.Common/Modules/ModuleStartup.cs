using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRExample.Common.Modules
{
    public abstract class ModuleStartup
    {
        public virtual int Order { get; } = 0;

        public virtual string Name => this.GetType().Namespace ?? "UnKnown";

        public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}