using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Collisions.Pusher
{
    public class PlayerPusherBehaviour : MonoBehaviour
    {
        private PlayerPusher _pusher;

        [Inject]
        private void Construct(PlayerPusher pusher)
        {
            _pusher = pusher;
            
            _pusher.Initialize(transform);
        }

        private void OnEnable()
        {
            _pusher.SubscribeToEvents();
        }

        private void OnDisable()
        {
            _pusher.UnsubscribeFromEvents();
        }
    }
}