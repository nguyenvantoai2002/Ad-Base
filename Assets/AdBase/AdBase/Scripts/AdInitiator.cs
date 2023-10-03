using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolffun.Ads
{
    public class AdInitiator : MonoBehaviour
    {
        [SerializeField] private bool _isAutoInit = true;
        [SerializeField] private AdFactoryBase _adFactory;

        [Space(16)]
        [SerializeField] private AdRequestController _adRequestController;
        [SerializeField] private AdControllerBase[] _adControllers;

        private bool _isInitCompleted = false;

        public bool IsInitCompleted => _isInitCompleted; 

        private void Awake()
        {
            if (_isAutoInit)
            {
                InitSDK().Forget();
            }
        }

        public async UniTask InitSDK()
        {
            //init SDK here
            bool isSuccess = await _adFactory.InitSDK();

            //TODO: handle error

            //request ad in queue
            foreach (AdControllerBase adController in _adControllers)
            {
                _adRequestController.RequestAd(adController);
            }

            _isInitCompleted = true;
        }
    }
}