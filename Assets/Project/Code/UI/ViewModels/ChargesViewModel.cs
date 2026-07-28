using System;
using MVVM;
using Project.Code.Gameplay.Player.Info;
using UniRx;
using Zenject;

namespace Project.Code.UI.ViewModels
{
    public class ChargesViewModel : IInitializable, IDisposable
    {
        [Data("Charges")] 
        public ReactiveProperty<string> Charges = new();

        private IDisposable _subscription;

        private PlayerInfo _playerInfo;

        public ChargesViewModel(PlayerInfo playerInfo)
        {
            _playerInfo = playerInfo;
        }

        public void Initialize()
        {
            _subscription = _playerInfo.Charges.Subscribe(OnChargesChanged);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnChargesChanged(int charges)
        {
            string text = $"charges: {charges}";
            Charges.Value = text;
        }
    }
}