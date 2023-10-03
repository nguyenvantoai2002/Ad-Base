using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Wolffun.Ads.MaxMediation
{
    [CreateAssetMenu(fileName = "CommonMaxMediationAdFactory", menuName = "ScriptableObjects/Ads/Factory/CommonMaxMediationAdFactory")]
    public class CommonMaxMediationAdFactory : AdFactoryBase
    {
        [Serializable]
        protected class MaxMediationAdUnitKeys
        {
            [SerializeField] protected AdOperationSystem _adPlatform;

            [SerializeField] protected string _maxMediationBannerID;
            [SerializeField] protected string _maxMediationInterstitialID;
            [SerializeField] protected string _maxMediationRewardID;

            public string MaxMediationBannerID => _maxMediationBannerID;
            public string MaxMediationInterstitialID => _maxMediationInterstitialID;
            public string MaxMediationRewardID => _maxMediationRewardID;
            public AdOperationSystem AdPlatform => _adPlatform;

            //public bool IsEmpty()
            //{
            //    return string.IsNullOrEmpty(_maxMediationBannerID) && 
            //        string.IsNullOrEmpty(_maxMediationInterstitialID) && 
            //        string.IsNullOrEmpty(_maxMediationRewardID);
            //}
        }

        [SerializeField] protected string _maxMediationSDKKey;

        [Space(12)]
        [SerializeField] protected MaxMediationAdUnitKeys[] _arrSupportedPlaformAdUnitKeys;
        [SerializeField] protected MaxMediationAdUnitKeys _editorAdUnitKeys;

        [NonSerialized] protected MaxMediationAdUnitKeys _chosenMaxMediationAdUnitKeys;

        protected MaxMediationAdUnitKeys ChosenMaxMediationAdUnitKeys
        {
            get
            {
                if (_chosenMaxMediationAdUnitKeys == null)
                {
                    _chosenMaxMediationAdUnitKeys = _editorAdUnitKeys;
                    AdOperationSystem curPlatform = AdOperationSystemChecker.GetAdOperationSystem();

                    foreach (MaxMediationAdUnitKeys adUnitKeys in _arrSupportedPlaformAdUnitKeys)
                    {
                        if (adUnitKeys.AdPlatform == curPlatform)
                        {
                            _chosenMaxMediationAdUnitKeys = adUnitKeys;
                            break;
                        }
                    }

                }

                return _chosenMaxMediationAdUnitKeys;
            }
        }

        public override async UniTask<bool> InitSDK()
        {
            UniTaskCompletionSource onInitComplete = new UniTaskCompletionSource();
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {
                onInitComplete.TrySetResult();
            };
            MaxSdk.SetSdkKey(_maxMediationSDKKey);
            MaxSdk.InitializeSdk();

#if MAX_MEDIATION_DEBUGGER 
            MaxSdk.ShowMediationDebugger();
#endif

            await onInitComplete.Task;

            return true;
        }

        public override BannerAdModelBase GetCommonBannerAd()
        {
            string maxMediationBannerID = ChosenMaxMediationAdUnitKeys.MaxMediationBannerID;
            if (string.IsNullOrEmpty(maxMediationBannerID))
            {
                return base.GetCommonBannerAd();
            }

            return new MaxMediationBannerAd(maxMediationBannerID);
        }

        public override InterstitialModelBase GetCommonForceAd()
        {
            string maxMediationInterstitialID = ChosenMaxMediationAdUnitKeys.MaxMediationInterstitialID;

            if (string.IsNullOrEmpty(maxMediationInterstitialID))
            {
                return base.GetCommonForceAd();
            }

            return new MaxMediationInterstitalAd(maxMediationInterstitialID);

        }

        public override RewardedAdModelBase GetCommonRewardedAd()
        {
            string maxMediationRewardID = ChosenMaxMediationAdUnitKeys.MaxMediationRewardID;

            if (string.IsNullOrEmpty(maxMediationRewardID))
            {
                return base.GetCommonRewardedAd();
            }

            return new MaxMediationRewardAd(maxMediationRewardID);

        }
    }

    

}