
namespace Toga.Ads.MaxMediation
{
    public class MaxMediationRewardAd : RewardedAdModelBase
    {
        protected string _maxMediationRewardedID;
        public MaxMediationRewardAd(string maxMediationRewardedID)
        {
            _maxMediationRewardedID = maxMediationRewardedID;

            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleRewardedVideoLoaded();
        }

        private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleRewardBasedVideoFailedToLoad(errorInfo.Message);
        }

        private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleRewardedVideoClosed();
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleRewardedVideoOpened();
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleOnAdRewarded();
        }

        public override void Request()
        {
            HandleStartRequestAd();
            MaxSdk.LoadRewardedAd(_maxMediationRewardedID);
        }

        public override void Show()
        {
            if (MaxSdk.IsRewardedAdReady(_maxMediationRewardedID))
            {
                MaxSdk.ShowRewardedAd(_maxMediationRewardedID);
            }
        }
    }
}

