using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.InputReading
{
    public class PlayerInputBehaviour : MonoBehaviour
    {
        private PlayerInput _input;

        [Inject]
        private void Construct(PlayerInput input)
        {
            _input = input;
        }

        private void Update()
        {
            _input.UpdateInput();
        }
    }
}