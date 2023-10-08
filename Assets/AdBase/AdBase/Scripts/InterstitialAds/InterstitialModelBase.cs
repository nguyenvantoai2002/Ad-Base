using System;

namespace Toga.Ads
{
    public abstract class InterstitialModelBase
    {
        protected int _loadFailedTimes;

        protected bool _isAdReady = false;
        protected bool _isRequesting = false;
        protected bool _isWaitingToRequest = false;

        protected Action<string> _onLoadedFail;
        protected Action _onLoadAdSuccess;
        protected Action _onAdClose;
        protected Action _onAdOpening;

        #region Get set
        public int LoadFailTime { get => _loadFailedTimes; }
        public bool AdReady { get => _isAdReady; }
        public bool IsRequesting { get => _isRequesting; }
        public bool IsWaitingToRequest { get => _isWaitingToRequest; }

        #endregion

        public void SetCallback(Action<string> onLoadedFail, Action onLoadAdSuccess, Action onAdClose, Action onAdOpening)
        {
            _onLoadedFail = onLoadedFail;
            _onLoadAdSuccess = onLoadAdSuccess;
            _onAdClose = onAdClose;
            _onAdOpening = onAdOpening;
        }

        public abstract void Request();
        public abstract bool IsLoaded();
        public abstract bool Show();

        protected void OnStartRequestAd()
        {
            _isWaitingToRequest = false;
            _isAdReady = false;
            _isRequesting = true;
        }

        protected virtual void OnFailedLoad(string error)
        {
            //AdManager.Instance.AddLoadFailedEvent(error, AdType.Force); //Analytic
            _isRequesting = false;
            _isWaitingToRequest = true;
            _loadFailedTimes++;

            _onLoadedFail?.Invoke(error);
        }

        protected virtual void OnLoadAdSuccess()
        {
            _isRequesting = false;
            _loadFailedTimes = 0;
            _isAdReady = true;

            _onLoadAdSuccess?.Invoke();
        }

        protected virtual void OnAdClose()
        {
            _isWaitingToRequest = true;

            _onAdClose?.Invoke();
        }

        protected virtual void OnAdOpen()
        {

            _onAdOpening?.Invoke();
        }
    }
}