using System;
using Wolffun.Log;

namespace Wolffun.Ads
{
    public abstract class RewardedAdModelBase
    {
        protected bool _isRequestingRewardVideo;
        protected bool _isWaitingRequestVideo;
        protected bool _isRewardedVideoLoaded;
        protected int _failedTimes;

        protected Action _onAdRewarded;
        protected Action _onLoadAdSuccess;
        protected Action<string> _onLoadAdFailed;
        protected Action _onAdOpening;
        protected Action _onAdClose;

        protected bool _isRequestingRewardedVideo;


        public bool IsWaitingRequestVideo => _isWaitingRequestVideo;
        public bool IsRewardedVideoLoaded => _isRewardedVideoLoaded;
        public int FailedTimes => _failedTimes;

        public void SetCallback(Action onAdRewarded, Action onLoadAdSuccess, Action<string> onLoadAdFailed, Action onAdOpening, Action onAdClose)
        {
            _onAdRewarded = onAdRewarded;
            _onLoadAdSuccess = onLoadAdSuccess;
            _onLoadAdFailed = onLoadAdFailed;
            _onAdOpening = onAdOpening;
            _onAdClose = onAdClose;
        }


        public abstract void Show();
        public abstract void Request();

        protected void HandleStartRequestAd()
        {
            _isWaitingRequestVideo = _isRewardedVideoLoaded = false;
            _isRequestingRewardedVideo = true;
        }

        public void HandleOnAdRewarded()
        {
            _onAdRewarded?.Invoke();
        }

        protected void HandleRewardedVideoLoaded()
        {
            _isRewardedVideoLoaded = true;
            _failedTimes = 0;
            _isRequestingRewardedVideo = false;

            _onLoadAdSuccess?.Invoke();
        }

        protected void HandleRewardBasedVideoFailedToLoad(string messageError)
        {
            CommonLog.Log(AdLog.GetLogString(messageError));

            _isRewardedVideoLoaded = false;
            _isRequestingRewardedVideo = false;
            _isWaitingRequestVideo = true;
            _failedTimes++;

            _onLoadAdFailed?.Invoke(messageError);
        }

        protected virtual void HandleRewardedVideoOpened()
        {
            _onAdOpening?.Invoke();
        }

        protected virtual void HandleRewardedVideoClosed()
        {
            _isWaitingRequestVideo = true;
            _isRewardedVideoLoaded = false;

            _onAdClose?.Invoke();
        }
    }
}