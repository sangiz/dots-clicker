using Assets.Scripts.Infrastructure.Installers.Plain;
using IgnSDK.Installers.Plain;
using Zenject;

namespace Assets.Scripts.Infrastructure.Installers.Mono
{
    public class GameMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            new ServicesInstaller(Container).InstallBindings();
            new GameplayServicesInstaller(Container).InstallBindings();
            new GameplayCreationalInstaller(Container).InstallBindings();
        }
    }
}
