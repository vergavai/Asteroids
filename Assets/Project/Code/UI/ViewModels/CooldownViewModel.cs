using System;
using System.Collections.Generic;
using MVVM;
using Project.Code.Gameplay.Player.Info;
using UniRx;
using Zenject;

namespace Project.Code.UI.ViewModels
{
    public class CooldownViewModel : IInitializable, IDisposable
    {
        [Data("Cooldown")] 
        public ReactiveProperty<string> Cooldown = new();

        private IDisposable _subscription;
        private PlayerInfo _playerInfo;

        public CooldownViewModel(PlayerInfo playerInfo)
        {
            _playerInfo = playerInfo;
        }

        public void Initialize()
        {
            _subscription = _playerInfo.LasersCooldown.Subscribe(OnCooldown);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnCooldown(IReadOnlyList<float> timers)
        {
            string text = "";
            for (int i = 0; i < timers.Count; i++)
            {
                string display = timers[i] == 0 ? "ready" : timers[i].ToString("F2");
                text += $"{i + 1} charge: {display}\n";
            }
            Cooldown.Value = text;
        }
    }
}