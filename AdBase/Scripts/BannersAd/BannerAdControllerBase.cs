using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toga.Ads
{
    public abstract class BannerAdControllerBase : AdControllerBase
    {
        public const int MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS = 7000; //7s

        protected bool _isFailedLoadBannerAd;

        protected abstract void CreateBannerAdModelBase();
        protected abstract void ShowBanner();
        protected abstract void DestroyBanner();
        public abstract void HideBanner();

        protected void SetCallbackToAdModel(BannerAdModelBase bannerAdModel)
        {
            if (bannerAdModel != null)
            {
                bannerAdModel.SetCallback(null, null, HandleAdLoadSuccess, HandleAdLoadFail);
            }
        }

        protected virtual void Awake()
        {
            CreateBannerAdModelBase();
            AdMemoryChecker.s_OnLowMemory += OnLowMemory;
        }

        protected virtual void OnDestroy()
        {
            AdMemoryChecker.s_OnLowMemory -= OnLowMemory;
        }


        //Use update and flag to handle instead of direct callback because the callback from may run on others threads that not from main thread
        //Action mostly need to change UI, that run on main thread --> Use update to sync action to main thread
        protected virtual void Update()
        {
            //when banner loading failed
            if (_isFailedLoadBannerAd)
            {
                _isFailedLoadBannerAd = false;
                RequestNewAdAfterSecons(MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS).Forget();
            }
        }

        protected virtual async UniTask RequestNewAdAfterSecons(int timeInMs)
        {
            await UniTask.Delay(timeInMs);

            RequestNewAd();

        }

        public virtual void StartShowAd()
        {
            ShowBanner();
        }

        public virtual void StartDestroyAd()
        {
            DestroyBanner();
        }

        protected virtual void HandleAdLoadSuccess()
        {
        }

        protected virtual void HandleAdLoadFail(string error)
        {
            _isFailedLoadBannerAd = true;
        }

        protected virtual void OnLowMemory()
        {
            AdMemoryChecker.s_OnLowMemory -= OnLowMemory;
            StartDestroyAd();
        }

    }
}