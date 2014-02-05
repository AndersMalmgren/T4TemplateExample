using System;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace MyApp.Web.Core.Ninject
{
    public class NinjectWebApiDependencyScope : IDependencyScope
    {
        IResolutionRoot resolver;

        public NinjectWebApiDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            return resolver.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            return resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
        }
    }
}
