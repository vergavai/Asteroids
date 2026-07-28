using GoogleMobileAds.Api;
using Project.Code.Gameplay.Player.Health;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Ads
{
    public class Ads : MonoBehaviour
    {
        private InterstitialAd _interstitial;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            MobileAds.Initialize(_ => { });
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDeath);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDeath);
        }

        private void OnPlayerDeath()
        {
            ShowInterstitial();
        }

        private void ShowInterstitial()
        {
            InterstitialAd.Load("id", new AdRequest(), (ad, error) =>
            {
                if (error != null)
                {
                    return;
                }
        
                _interstitial = ad;
                _interstitial.Show();
            });
        }
    }
}