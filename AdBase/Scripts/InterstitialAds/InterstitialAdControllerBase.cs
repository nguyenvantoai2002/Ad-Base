using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolffun.Log;
using Wolffun.Singleton;

namespace Wolffun.Ads
{
    public abstract class InterstitialAdControllerBase : AdControllerBase
    {
        public const int MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS = 12000; //10s
        public const int INTERVAL_LOAD_NEXT_AD_AFTER_WATCH_IN_MS = 1000; //1s

        protected bool _isOnClosedForcedInterestial;
        protected bool _isOnLoadedFailed;

        protected Action _onCloseForceInterstial;
        protected Action _onFailToShowInterstial;
        protected Action _onOpenInterstial;

        protected abstract void CreateInterstitialAdModelBase();
        protected abstract void ShowAd();

        protected void SetCallbackToAdModel(InterstitialModelBase forceAdModel)
        {
            if (forceAdModel != null)
            {
                forceAdModel.SetCallback(HandleLoadedFail, null, HandleForceInterstialAdClosed, HandleAdOpening);
            }

        }

        //don't need unregister callback, the callback is auto clear all when condition met
        #region Register call back
        public InterstitialAdControllerBase RegisterCloseAdCallback(Action onCloseForceInterstial)
        {
            _onCloseForceInterstial += onCloseForceInterstial;
            return this;
        }

        public InterstitialAdControllerBase RegisterFailToShowAdCallback(Action onFailToShowInterstial)
        {
            _onFailToShowInterstial += onFailToShowInterstial;
            return this;
        }

        public InterstitialAdControllerBase RegisterOpenAdCallback(Action onOpenInterstial)
        {
            _onOpenInterstial += onOpenInterstial;
            return this;
        }


        #endregion

        protected virtual void Awake()
        {
            CreateInterstitialAdModelBase();

            //            if (mInterstitalAds == null)
            //            {
            //                mInterstitalAds = new InterstitialModelBase[1];

            //                InterstitialModelBase adModel = null;

            //#if APP_LOVIN
            //                adModel = new ApplovinInterstitalAd();
            //#endif

            //#if AD_MOB
            //                adModel = new AdmodInterstitalAd();
            //#endif

            //                if (adModel != null)
            //                {
            //                    adModel.SetCallback(HandleLoadedFail, null, HandleForceInterstialAdClosed, HandleAdOpening);
            //                }

            //                mInterstitalAds[0] = adModel;
            //            }

        }

        //Use update and flag to handle instead of direct callback because the callback from may run on others threads that not from main thread
        //Action mostly need to change UI, that run on main thread --> Use update to sync action to main thread
        protected virtual void Update()
        {
            //when ad is closed
            if (_isOnClosedForcedInterestial)
            {
                _isOnClosedForcedInterestial = false;
                _onCloseForceInterstial?.Invoke();
                _onCloseForceInterstial = null;
                _onFailToShowInterstial = null;
                RequestNewAdAfterSecons(INTERVAL_LOAD_NEXT_AD_AFTER_WATCH_IN_MS).Forget();
            }

            //when ad loaded failed
            if (_isOnLoadedFailed)
            {
                _isOnLoadedFailed = false;
                RequestNewAdAfterSecons(MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS).Forget();
            }
        }

        protected virtual void HandleForceInterstialAdClosed()
        {
            _isOnClosedForcedInterestial = true;
            _onOpenInterstial = null;

            AdminLog.LogError(AdLog.GetLogString("interstitial ad close"));
        }

        protected virtual void HandleLoadedFail(string error)
        {
            _isOnLoadedFailed = true;
            _onFailToShowInterstial?.Invoke();
            _onFailToShowInterstial = null;
            _onCloseForceInterstial = null;
            _onOpenInterstial = null;
        }

        protected virtual void HandleAdOpening()
        {
            _onOpenInterstial?.Invoke();
            _onOpenInterstial = null;
        }


        public virtual bool StartShowAd()
        {
            AdminLog.LogError(AdLog.GetLogString("interstitial start show ad"));

            if (!IsLoadedAd())
            {
                CommonLog.Log(AdLog.GetLogString("interstitial ad shown error"));
                _onCloseForceInterstial = null;
                _onFailToShowInterstial = null;
                _onOpenInterstial = null;
                return false;
            }

            ShowAd();

            return true;
        }

        protected virtual async UniTask RequestNewAdAfterSecons(int timeInMs)
        {
            await UniTask.Delay(timeInMs);

            RequestNewAd();

        }

    }
}