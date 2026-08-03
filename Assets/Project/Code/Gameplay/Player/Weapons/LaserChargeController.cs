using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Code.Configs;
using UnityEngine;

namespace Project.Code.Gameplay.Player.Weapons
{
    public class LaserChargeController : IDisposable
    {
        private readonly PlayerConfig _config;
        private CancellationTokenSource _rechargeTokenSource;
        
        private int _maxCharges;
        private int _currentCharges;
        private float _currentCooldown;
        private float[] _chargeTimers;
        private bool _isRegenerating;

        public int CurrentCharges => _currentCharges;
        public IReadOnlyList<float> ChargeTimers => Array.AsReadOnly(_chargeTimers);

        public LaserChargeController(PlayerConfig config)
        {
            _config = config;
            _maxCharges = _config.LaserMaxCharges;
            _currentCooldown = _config.LaserCooldown;
            _currentCharges = _maxCharges;
            _chargeTimers = new float[_maxCharges];
            _rechargeTokenSource = new CancellationTokenSource();
        }

        public bool TryConsumeCharge(out int slotIndex)
        {
            slotIndex = -1;
            if (_currentCharges <= 0) return false;

            for (int i = 0; i < _chargeTimers.Length; i++)
            {
                if (Mathf.Approximately(_chargeTimers[i], 0f))
                {
                    slotIndex = i;
                    break;
                }
            }

            if (slotIndex == -1) return false;

            _currentCharges--;
            _chargeTimers[slotIndex] = _currentCooldown;
            StartRegenerationIfNeeded();
            return true;
        }

        public void ReturnCharge(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _chargeTimers.Length) return;
            _currentCharges++;
            _chargeTimers[slotIndex] = 0f;
        }

        private void StartRegenerationIfNeeded()
        {
            if (!_isRegenerating && _rechargeTokenSource is { IsCancellationRequested: false })
            {
                _isRegenerating = true;
                RegenerateChargesAsync(_rechargeTokenSource.Token).Forget();
            }
        }

        private async UniTask RegenerateChargesAsync(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();

                    bool anyActive = false;
                    for (int i = 0; i < _chargeTimers.Length; i++)
                    {
                        if (_chargeTimers[i] > 0f)
                        {
                            _chargeTimers[i] -= Time.deltaTime;
                            if (_chargeTimers[i] <= 0f)
                            {
                                _chargeTimers[i] = 0f;
                                _currentCharges++;
                            }
                            anyActive = true;
                        }
                    }

                    if (!anyActive)
                        break;

                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _isRegenerating = false;
            }
        }

        public void Dispose()
        {
            _rechargeTokenSource?.Cancel();
            _rechargeTokenSource?.Dispose();
        }
    }
}