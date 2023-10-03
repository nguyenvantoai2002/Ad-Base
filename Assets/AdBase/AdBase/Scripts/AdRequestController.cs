using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolffun.Ads
{
    public class AdRequestController : MonoBehaviour
    {
        [SerializeField] private AdInitiator _adInitiator;

        private int _timeToWaitForNextRequestInMs = 2500;
        private int _timeToWaitForNextCheckNetworkInMs = 2500;

        private Queue<AdControllerBase> _queueToLoadAds = new Queue<AdControllerBase>(3);

        public void RequestAd(AdControllerBase adControllerBase)
        {
            //don't load any more ads if device is weak
            if (AdMemoryChecker.IsDeviceWeak())
            {
                return;
            }

            //add to queue waiting for requesting ad
            _queueToLoadAds.Enqueue(adControllerBase);

            //only call unitask again when there are no previous ads in queue
            if (_queueToLoadAds.Count == 1)
            {
                RequestAdInQueue().Forget();
            }

        }

        private async UniTask RequestAdInQueue()
        {
            //request ads until there are no request left
            while (_queueToLoadAds.Count > 0)
            {
                if (!_adInitiator.IsInitCompleted)
                {
                    await UniTask.WaitUntil(()=> _adInitiator.IsInitCompleted);
                }

                await WaitUntilConnectToNetwork();
                AdControllerBase adControllerToLoad = _queueToLoadAds.Peek();
                adControllerToLoad.Request();

                await UniTask.Delay(_timeToWaitForNextRequestInMs);

                _queueToLoadAds.Dequeue();
            }
        }

        private async UniTask WaitUntilConnectToNetwork()
        {
            while (true)
            {
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    return;
                }

                await UniTask.Delay(_timeToWaitForNextCheckNetworkInMs);

            }

        }
    }
}