using FluentValidation;
using MediatR;
using MediatRExample.Common.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRExample.Common.Modules
{
    public static class ModulesHelper
    {
        public static void AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var startup in GetAllModuleStartupInstances())
            {
                // register all MediatR types from the module assembly
                services.AddMediatR(startup.GetType());

                // register all Validators from the module assembly
                services.AddValidatorsFromAssembly(startup.GetType().Assembly);

                // Register additional services from the module startup
                startup.ConfigureServices(services, configuration);
            }
        }

        private static IEnumerable<ModuleStartup> GetAllModuleStartupInstances()
        {
            var moduleAssemblies = AppDomain.CurrentDomain.GetAssemblies().GetAllReferencedModuleAssemblies();
            var moduleStartupTypes = moduleAssemblies.SelectMany(a => a.GetAllModuleStartupTypes());
            return moduleStartupTypes
                .Select(startupType => (ModuleStartup?)Activator.CreateInstance(startupType) ?? null)
                .Where(startup => startup != null)
                .OrderBy(m => m!.Order)
                .ToList()!;
        }
    }
}