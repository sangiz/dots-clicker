using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Infrastructure.Installers.Plain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IgnSDK.Installers.Plain
{
    public class GameplayCreationalInstaller : PlainAbstractInstaller
    {
        public GameplayCreationalInstaller(DiContainer container) : base(container)
        {

        }

        public override void InstallBindings()
        {
            //! Bind all object created at runtime

            BindUIFactory();
        }

        private void BindUIFactory()
        {
            Container
                .BindInterfacesTo<UiFactory>()
                .AsSingle();
        }
    }
}
