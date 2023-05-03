using Assets.Scripts.UI.Widgets;
using IgnSDK.Infrastructure.Services;

namespace Assets.Scripts.Infrastructure.Factories
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssets assets;

        private ConnectablePointWidget connectablePointWidgetPrefab;
        private LevelSelectionButton levelSelectionButton;

        public UiFactory(IAssets assets)
        {
            this.assets = assets;

            LoadResources();
        }

        private void LoadResources()
        {
            connectablePointWidgetPrefab = assets.GetPrefab<ConnectablePointWidget>(Constants.Prefabs.UI.ConnectablePointWidget);
            levelSelectionButton = assets.GetPrefab<LevelSelectionButton>(Constants.Prefabs.UI.LevelSelectionButton);
        }

        public LevelSelectionButton CreateLevelSelectionButton() =>
            assets.Instantiate<LevelSelectionButton>(levelSelectionButton.gameObject);

        public ConnectablePointWidget CreateConnectablePointWidget() =>
            assets.Instantiate<ConnectablePointWidget>(connectablePointWidgetPrefab.gameObject);
    }
}
