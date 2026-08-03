using Project.Code.Gameplay.Player.InputReading;
using Project.Code.UI.Visibility;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private ForceMobileCheck _forceMobile;

        public override void InstallBindings()
        {
            BindInputProvider();
        }
        
        private void BindInputProvider()
        {
            if (!_forceMobile.ForceMobileInput)
            {
                Container.Bind<IInputProvider>()
                    .To<DesktopInputProvider>()
                    .FromNew()
                    .AsSingle()
                    .NonLazy();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<MobileInputProvider>()
                    .FromNew()
                    .AsSingle()
                    .NonLazy();
            }
        }
    }
}