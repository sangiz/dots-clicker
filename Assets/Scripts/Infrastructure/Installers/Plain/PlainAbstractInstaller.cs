using Zenject;

namespace Assets.Scripts.Infrastructure.Installers.Plain
{
    public abstract class PlainAbstractInstaller
    {
        protected readonly DiContainer Container;

        public PlainAbstractInstaller(DiContainer container)
        {
            Container = container;
        }

        public abstract void InstallBindings();
    }
}
