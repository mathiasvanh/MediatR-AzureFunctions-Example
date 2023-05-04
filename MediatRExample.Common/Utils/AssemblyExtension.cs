using System.Reflection;
using MediatRExample.Common.Modules;

namespace MediatRExample.Common.Utils
{
    public static class AssemblyExtension
    {
        public static IEnumerable<Type> GetAllModuleStartupTypes(this Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ModuleStartup)));
        }

        public static IEnumerable<Assembly> GetAllReferencedModuleAssemblies(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.GetAllReferencedMediatRExampleAssemblies()
                .Where(a => a.FullName?.StartsWith("MediatRExample.Modules") ?? false);

        }

        public static List<Assembly> GetAllReferencedMediatRExampleAssemblies(this IEnumerable<Assembly> assemblies)
        {
            var result = new List<Assembly>();
            var loadedAssemblies = new HashSet<string>();
            var assembliesToCheck = new Queue<Assembly>();

            foreach (var assembly in assemblies)
            {
                assembliesToCheck.Enqueue(assembly);
            }

            while(assembliesToCheck.Count > 0)
            {
                var assemblyToCheck = assembliesToCheck.Dequeue();

                foreach(var reference in assemblyToCheck.GetReferencedAssemblies())
                {
                    if (loadedAssemblies.Contains(reference.FullName)) 
                        continue;
                    
                    if (!reference.FullName.StartsWith("MediatRExample."))
                        continue;
                    
                    var assembly = Assembly.Load(reference);
                    assembliesToCheck.Enqueue(assembly);
                    loadedAssemblies.Add(reference.FullName);
                    result.Add(assembly);
                }
            }

            return result;
        }
    }
}