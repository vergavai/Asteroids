using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Rotating
{
    public class PlayerRotationBehaviour : MonoBehaviour
    {
        private PlayerRotation _playerRotation;
        
        [Inject]
        private void Construct(PlayerRotation playerRotation)
        {
            _playerRotation = playerRotation;
            
            _playerRotation.Initialize(transform);
        }

        private void Update()
        {
            _playerRotation.UpdateRotation();
        }
    }
}