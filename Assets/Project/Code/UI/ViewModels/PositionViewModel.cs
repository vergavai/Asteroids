using System;
using MVVM;
using Project.Code.Gameplay.Player.Info;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Code.UI.ViewModels
{
    public class PositionViewModel : IInitializable, IDisposable
    {
        [Data("Position")] 
        public ReactiveProperty<string> Position = new();

        private IDisposable _subscription;

        private PlayerInfo _playerInfo;

        public PositionViewModel(PlayerInfo playerInfo)
        {
            _playerInfo = playerInfo;
        }

        public void Initialize()
        {
            _subscription = _playerInfo.Position.Subscribe(OnPositionChanged);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnPositionChanged(Vector2 position)
        {
            string text = $"x: {Math.Round(position.x, 2)}\ny: {Math.Round(position.y, 2)}";
            Position.Value = text;
        }
    }
}