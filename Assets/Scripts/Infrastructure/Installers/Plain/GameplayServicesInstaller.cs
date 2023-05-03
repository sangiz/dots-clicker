using Assets.Scripts.Game;
using Assets.Scripts.Infrastructure.Installers.Plain;
using Assets.Scripts.Infrastructure.Services.JSonReader;
using Assets.Scripts.UI.Dialogs.Core;
using Zenject;

namespace IgnSDK.Installers.Plain
{
    public class GameplayServicesInstaller : PlainAbstractInstaller
    {
        public GameplayServicesInstaller(DiContainer container) : base(container)
        {
    
        }

        public override void InstallBindings()
        {
            BindGameplayManager();
            BindJsonReader();
            BindUiManager();
        }

        private void BindGameplayManager()
        {
            Container
                .BindInterfacesTo<GameplayManager>()
                .AsSingle();
        }

        private void BindUiManager()
        {
            Container
                .BindInterfacesTo<UiManager>()
                .AsSingle();
        }

        private void BindJsonReader()
        {
            Container
                .BindInterfacesTo<JsonReaderService>()
                .AsSingle();
        }
    }
}
