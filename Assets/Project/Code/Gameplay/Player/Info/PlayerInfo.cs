using System;
using System.Collections.Generic;
using Project.Code.Gameplay.Player.Movement;
using Project.Code.Gameplay.Player.Rotating;
using Project.Code.Gameplay.Player.Weapons;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Info
{
    public class PlayerInfo : ITickable
    {
        private readonly PlayerRotation _rotation;
        private readonly PlayerMovement _movement;
        private readonly LaserChargeController _laserChargeController;
        private readonly Transform _transform;
        private readonly PlayerMovementBehaviour _player;

        private readonly ReactiveProperty<float> _angle = new();
        private readonly ReactiveProperty<float> _speed = new();
        private readonly ReactiveProperty<int> _charges = new();
        private readonly ReactiveProperty<Vector2> _position = new();
        private Subject<IReadOnlyList<float>> _lasersCooldownSubject = new();

        public IReadOnlyReactiveProperty<float> Angle => _angle;
        public IReadOnlyReactiveProperty<float> Speed => _speed;
        public IReadOnlyReactiveProperty<Vector2> Position => _position;
        public IReadOnlyReactiveProperty<int> Charges => _charges;
        public IObservable<IReadOnlyList<float>> LasersCooldown => _lasersCooldownSubject;

        public PlayerInfo(PlayerRotation rotation, PlayerMovement movement, PlayerMovementBehaviour player, LaserChargeController laserChargeController)
        {
            _player = player;
            _rotation = rotation;
            _movement = movement;
            _transform = _player.transform;
            _laserChargeController = laserChargeController;

            _angle.Value = _rotation.Angle;
            _speed.Value = _movement.CurrentSpeed;
            _position.Value = _transform.position;
            _charges.Value = _laserChargeController.CurrentCharges;
        }

        public void Tick()
        {
            Refresh();
        }

        private void Refresh()
        {
            if(!_player)
                return;
            
            _angle.Value = _rotation.Angle;
            _speed.Value = _movement.CurrentSpeed;
            _position.Value = _transform.position;
            _charges.Value = _laserChargeController.CurrentCharges;
            _lasersCooldownSubject.OnNext(_laserChargeController.ChargeTimers);
        }
    }
}