using System.Reflection;
using AutoMapper;
using MediatRExample.Common.Utils;

namespace MediatRExample.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().GetAllReferencedMediatRExampleAssemblies())
            {
                ApplyMappingsFromAssembly(assembly);
            }
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMap<,>)))
                .ToList();

            foreach (var type in mapTypes)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("AddMapping")
                                 ?? type.GetInterface("IMap`2")!.GetMethod("AddMapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
            
            var mapFromTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in mapFromTypes)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("AddMapping")
                                 ?? type.GetInterface("IMapFrom`1")!.GetMethod("AddMapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}