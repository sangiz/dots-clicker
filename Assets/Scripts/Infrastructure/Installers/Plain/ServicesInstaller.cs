using Zenject;

namespace Assets.Scripts.Infrastructure.Installers.Plain
{
    public class ServicesInstaller : PlainAbstractInstaller
    {
        public ServicesInstaller(DiContainer container) : base(container)
        {
        }

        public override void InstallBindings()
        {
            BindAssets();
        }

        private void BindAssets()
        {
            Container
                .BindInterfacesTo<Services.AssetManagement.Assets>()
                .AsSingle();
        }
    }
}
