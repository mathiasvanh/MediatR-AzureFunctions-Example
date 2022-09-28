using Microsoft.Extensions.DependencyInjection;

namespace MediatRExample.Common
{
    public class AppDependencyResolver
    {
        private static AppDependencyResolver? _resolver;
        private readonly IServiceProvider _serviceProvider;

        public static AppDependencyResolver Current
        {
            get
            {
                if (_resolver == null)
                {
                    throw new Exception("AppDependencyResolver not initialized. You should initialize it in Startup class");
                }

                return _resolver;
            }
        }

        public static void Init(IServiceProvider services)
        {
            _resolver = new AppDependencyResolver(services);
        }   

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        private AppDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
