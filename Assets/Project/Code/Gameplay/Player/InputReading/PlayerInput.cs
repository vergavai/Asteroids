using System;
using Project.Code.Gameplay.Player.Collisions.Invincibility;
using UnityEngine;
using Input = UnityEngine.Input;

namespace Project.Code.Gameplay.Player.InputReading
{
    public class PlayerInput
    {
        private PlayerInvincibility _invincibility;
        private bool _isMoving;
        

        public event Action MoveStarted;
        public event Action MoveEnded;
        public event Action ShootRequested;
        public event Action LaserRequested;
        
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        public PlayerInput(PlayerInvincibility invincibility)
        {
            _invincibility = invincibility;
        }

        public void UpdateInput()
        {
            if (_invincibility.IsInvincible)
            {
                HorizontalInput = 0;
                VerticalInput = 0;
                return;
            }
            
            HorizontalInput = Input.GetAxis("Horizontal");
            VerticalInput = Input.GetAxis("Vertical");

            bool isMovingNow = Mathf.Abs(HorizontalInput) > 0.01f || Mathf.Abs(VerticalInput) > 0.01f;

            if (isMovingNow && !_isMoving)
                MoveStarted?.Invoke();
            else if (!isMovingNow && _isMoving)
                MoveEnded?.Invoke();

            _isMoving = isMovingNow;

            if (Input.GetMouseButtonDown(0))
                ShootRequested?.Invoke();
            if (Input.GetMouseButtonDown(1))
                LaserRequested?.Invoke();
        }
    }
}