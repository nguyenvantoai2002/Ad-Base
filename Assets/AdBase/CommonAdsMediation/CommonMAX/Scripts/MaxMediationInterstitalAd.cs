
namespace Toga.Ads.MaxMediation
{
    public class MaxMediationInterstitalAd : InterstitialModelBase
    {
        protected string _maxMediationInterstitialID;
        public MaxMediationInterstitalAd(string maxMediationInterstitialID)
        {
            _maxMediationInterstitialID = maxMediationInterstitialID;

            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
        }
        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            OnLoadAdSuccess();
        }

        private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            OnFailedLoad(errorInfo.Message);
        }

        private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            OnAdOpen();
        }

        private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

        }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            OnAdClose();
        }

        private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }
        }

        public override bool IsLoaded()
        {
            return MaxSdk.IsInterstitialReady(_maxMediationInterstitialID);
        }

        public override void Request()
        {
            OnStartRequestAd();

            MaxSdk.LoadInterstitial(_maxMediationInterstitialID);
        }

        public override bool Show()
        {
            if (IsLoaded())
            {
                MaxSdk.ShowInterstitial(_maxMediationInterstitialID);
                return true;
            }

            return false;
        }

    }
}