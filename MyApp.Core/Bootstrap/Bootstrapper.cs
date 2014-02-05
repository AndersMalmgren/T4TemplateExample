using MyApp.Core.CommandQuery;
using Ninject;

namespace MyApp.Core.Bootstrap
{
    public static class Bootstrapper
    {
        public static IKernel Create()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IClient>().To<Client>();

            return kernel;
        }
    }
}
