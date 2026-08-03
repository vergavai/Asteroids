using System;
using UnityEngine;

namespace Project.Code.Gameplay.Player.InputReading
{
    public class DesktopInputProvider : IInputProvider
    {
        private Camera _camera;

        public event Action ShootPerformed;
        public event Action LaserPerformed;

        public DesktopInputProvider(Camera camera)
        {
            _camera = camera;
        }

        public Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        public Vector2 GetAimTarget(Vector2 playerPosition)
        {
            Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            return mouseWorldPos;
        }
        
        public void UpdateInput()
        {
            if (Input.GetMouseButtonDown(0)) ShootPerformed?.Invoke();
            if (Input.GetMouseButtonDown(1)) LaserPerformed?.Invoke();
        }
    }
}