using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Weapons
{
    public class PlayerGunBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _gunPoint;
        [SerializeField] private Transform _laserPoint;
        
        private PlayerGun _gun;

        [Inject]
        private void Construct(PlayerGun gun)
        {
            _gun = gun;
            _gun.Initialize(_gunPoint, _laserPoint);
        }

        private void OnEnable()
        {
            _gun.SubscribeToEvents();
        }

        private void OnDisable()
        {
            _gun.UnsubscribeFromEvents();
        }
    }
}
