using System;
using MVVM;
using Project.Code.Gameplay.Player.Info;
using UniRx;
using Zenject;

namespace Project.Code.UI.ViewModels
{
    public class RotationViewModel : IInitializable, IDisposable
    {
        [Data("RotationAngle")] 
        public ReactiveProperty<string> Angle = new();

        private IDisposable _subscription;

        private PlayerInfo _playerInfo;

        public RotationViewModel(PlayerInfo playerInfo)
        {
            _playerInfo = playerInfo;
        }

        public void Initialize()
        {
            _subscription = _playerInfo.Angle.Subscribe(OnRotationChanged);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnRotationChanged(float angle)
        {
            string text = $"angle: {Math.Round(angle, 1)}";
            Angle.Value = text;
        }
    }   
}