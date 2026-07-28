using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Movement
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
            
            _playerMovement.Initialize(transform);
        }

        private void Update()
        {
            _playerMovement.UpdateMovement();
        }
    }
}   
