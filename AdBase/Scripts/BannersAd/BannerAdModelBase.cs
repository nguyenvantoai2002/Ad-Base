using System;

namespace Wolffun.Ads
{
    public abstract class BannerAdModelBase
    {
        protected bool _isLoaded = false;
        protected bool _isShow = false;

        protected bool _isRequesting = false;

        protected int _failedTimes = 0;

        protected Action _onAdHide;
        protected Action _onAdShow;
        protected Action _onAdLoaded;
        protected Action<string> _onAdLoadedFailed;

        #region Get set
        public bool IsRequesting => _isRequesting;
        public bool IsLoaded => _isLoaded;
        public bool IsShow => _isShow;

        #endregion


        public abstract void Request();
        public abstract void Show();
        public abstract void Destroy();
        public abstract void Hide();

        public void SetCallback(Action onAdHide, Action onAdShow, Action onAdLoaded, Action<string> onAdLoadedFailed)
        {
            _onAdHide = onAdHide;
            _onAdShow = onAdShow;
            _onAdLoaded = onAdLoaded;
            _onAdLoadedFailed = onAdLoadedFailed;
        }

        protected virtual void HandleAdHide()
        {
            _isShow = false;

            _onAdHide?.Invoke();
        }

        protected virtual void HandleAdShow()
        {
            _isShow = true;

            _onAdShow?.Invoke();
        }

        protected virtual void HandleAdLoaded()
        {
            _failedTimes = 0;
            _isLoaded = true;

            _onAdLoaded?.Invoke();
            //BannerAdManager.Instance.AdLoadSuccess();
        }

        protected virtual void HandleAdLoadFail(string error)
        {
            _failedTimes++;
            _isLoaded = false;

            _onAdLoadedFailed?.Invoke(error);
            //BannerAdManager.isFailedLoadBannerAd = true;
        }
    }
}