using IgnSDK.Infrastructure.Services;

namespace Assets.Scripts.Infrastructure.Factories
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssets assets;

        public UiFactory(IAssets assets)
        {
            this.assets = assets;

            LoadResources();
        }

        private void LoadResources()
        {
        }
    }
}
