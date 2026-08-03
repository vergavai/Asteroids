using System;
using UnityEngine;

namespace Project.Code.Gameplay.Player.InputReading
{
    public interface IInputProvider
    {
        Vector2 GetMovementInput();
        Vector2 GetAimTarget(Vector2 playerPosition);
        void UpdateInput(); 

        event Action ShootPerformed;
        event Action LaserPerformed;
    }
}