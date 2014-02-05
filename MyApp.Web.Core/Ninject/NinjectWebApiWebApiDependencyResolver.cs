using System.Web.Http.Dependencies;
using Ninject;

namespace MyApp.Web.Core.Ninject
{
    public class NinjectWebApiWebApiDependencyResolver : NinjectWebApiDependencyScope, IDependencyResolver
    {
        IKernel kernel;

        public NinjectWebApiWebApiDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectWebApiDependencyScope(kernel);
        }
    }
}