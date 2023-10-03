using UnityEngine;

namespace Wolffun.Ads.MaxMediation
{
    public class MaxMediationBannerAd : BannerAdModelBase
    {
        protected string _maxMediationBannerID;

        public MaxMediationBannerAd(string maxMediationBannerID)
        {
            _maxMediationBannerID = maxMediationBannerID;

            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnAdLoaded;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnAdFailedToLoad;
        }

        private void OnAdLoaded(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HandleAdLoaded();
            Show();
        }

        private void OnAdFailedToLoad(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            HandleAdLoadFail(errorInfo.Message);
        }

        public override void Destroy()
        {
            MaxSdk.DestroyBanner(_maxMediationBannerID);
        }

        public override void Request()
        {
            // Banners are automatically sized to 320x50 on phones and 728x90 on tablets
            // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments
            MaxSdk.CreateBanner(_maxMediationBannerID, MaxSdkBase.BannerPosition.BottomCenter);

            // Set background or background color for banners to be fully functional
            MaxSdk.SetBannerBackgroundColor(_maxMediationBannerID, Color.white);
        }

        public override void Show()
        {
            MaxSdk.ShowBanner(_maxMediationBannerID);
            HandleAdShow();
        }

        public override void Hide()
        {
            MaxSdk.HideBanner(_maxMediationBannerID);
            HandleAdHide();
        }
    }
}
