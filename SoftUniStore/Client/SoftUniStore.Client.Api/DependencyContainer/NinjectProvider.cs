namespace SoftUniStore.Client.Api.DependencyContainer
{
    using Ninject;

    public class NinjectProvider
    {
        private static IKernel kernel;

        public static IKernel Kernel() => kernel ?? (kernel = new StandardKernel(new Bindings()));
    }
}