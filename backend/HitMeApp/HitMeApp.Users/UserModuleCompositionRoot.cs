using Autofac;

namespace HitMeApp.Users
{
    internal static class UserModuleCompositionRoot
    {
        private static IContainer _container;

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static ILifetimeScope BeginLifetimeScope()
            => _container.BeginLifetimeScope();
    }
}
