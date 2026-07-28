using System;
using MVVM;
using Project.Code.Gameplay.Player.Info;
using UniRx;
using Zenject;

namespace Project.Code.UI.ViewModels
{
    public class SpeedViewModel : IInitializable, IDisposable
    {
        [Data("Speed")] 
        public ReactiveProperty<string> Speed = new();

        private IDisposable _subscription;

        private PlayerInfo _playerInfo;

        public SpeedViewModel(PlayerInfo playerInfo)
        {
            _playerInfo = playerInfo;
        }

        public void Initialize()
        {
            _subscription = _playerInfo.Speed.Subscribe(OnSpeedChanged);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnSpeedChanged(float speed)
        {
            string text = $"speed: {Math.Round(speed, 2)}";
            Speed.Value = text;
        }
    }
}