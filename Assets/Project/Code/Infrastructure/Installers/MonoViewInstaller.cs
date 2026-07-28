using MVVM;
using Project.Code.UI.Binders;
using Project.Code.UI.ViewModels;
using Zenject;

namespace Project.Code.Infrastructure.Installers
{
    public class MonoViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<RotationViewModel>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<SpeedViewModel>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<PositionViewModel>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ChargesViewModel>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<CooldownViewModel>()
                .AsSingle()
                .NonLazy();
            
            BinderFactory.RegisterBinder<TextBinder>();
        }
    }
}