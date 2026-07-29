using Project.Code.Gameplay.Player.Collisions.Invincibility;
using Project.Code.Gameplay.Player.InputReading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Code.UI.Buttons
{
    public class ShootButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private bool _isLaser;

        private MobileInputProvider _inputProvider;
        private PlayerInvincibility _invincibility;

        [Inject]
        private void Construct([InjectOptional] MobileInputProvider provider, PlayerInvincibility invincibility)
        {
            _inputProvider = provider;
            _invincibility = invincibility;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonPressed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonPressed);
        }

        private void OnButtonPressed()
        {
            if (_inputProvider == null) return;
            if (_invincibility != null && _invincibility.IsInvincible) return;

            if (_isLaser)
                _inputProvider.SetLaserPressed();
            else
                _inputProvider.SetShootPressed();
        }
    }
}