using Autofac;

namespace HitMeApp.Indentity
{
    internal static class IdentityModuleCompositionRoot
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
