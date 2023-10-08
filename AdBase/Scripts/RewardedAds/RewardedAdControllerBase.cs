using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolffun.Ads
{
    public abstract class RewardedAdControllerBase : AdControllerBase
    {
        public const int MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS = 5000; //5s
        public const int INTERVAL_LOAD_NEXT_AD_AFTER_WATCH_IN_MS = 1000; //1s

        //RewardedVideo
        protected bool _isOnFailedVideoLoaded;

        protected bool _isOnWatchedRewarededSuccessed;

        protected bool _isOnClosedRewardedVideo;

        //Action
        protected Action _onSuccessWatchVideo;
        protected Action _onCloseRewardedVideo;
        protected Action _onOpenRewardedVideo;

        protected abstract void CreateRewardAdModelBase();
        protected abstract void ShowAd();

        protected void SetCallbackToAdModel(RewardedAdModelBase rewardAdModel)
        {
            if (rewardAdModel != null)
            {
                rewardAdModel.SetCallback(HandleWatchVideoSuccess, null, HandleAdLoadedFailed, HandleAdOpen, HandleRewardedVideoClosed);
            }
        }

        //don't need unregister callback, the callback is auto clear all when condition met
        #region Register call back
        public RewardedAdControllerBase RegisterSuccessWatchAdAdCallback(Action onSuccessWatchVideo)
        {
            _onSuccessWatchVideo += onSuccessWatchVideo;
            return this;
        }

        public RewardedAdControllerBase RegisterCloseRewardAdCallback(Action onCloseRewardedVideo)
        {
            _onCloseRewardedVideo += onCloseRewardedVideo;
            return this;
        }

        public RewardedAdControllerBase RegisterOpenAdCallback(Action onOpenRewardedVideo)
        {
            _onOpenRewardedVideo += onOpenRewardedVideo;
            return this;
        }


        #endregion


        protected virtual void Awake()
        {
            CreateRewardAdModelBase();
        }

        public virtual bool StartShowAd()
        {
            if (!IsLoadedAd())
            {
                _onSuccessWatchVideo = null;
                _onCloseRewardedVideo = null;
                _onOpenRewardedVideo = null;

                return false;
            }

            ShowAd();

            return true;
        }

        //Use update and flag to handle instead of direct callback because the callback from may run on others threads that not from main thread
        //Action mostly need to change UI, that run on main thread --> Use update to sync action to main thread
        protected virtual void Update()
        {
            //when ad loaded fail
            if (_isOnFailedVideoLoaded)
            {
                _isOnFailedVideoLoaded = false;
                //OnFailedLoadedRewardedVideo();
                RequestNewAdAfterSeconds(MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS).Forget();
            }
            
            //when ad close
            if (_isOnClosedRewardedVideo)
            {
                _isOnClosedRewardedVideo = false;

                _onCloseRewardedVideo?.Invoke();
                _onCloseRewardedVideo = null;
                _onOpenRewardedVideo = null;

                RequestNewAdAfterSeconds(INTERVAL_LOAD_NEXT_AD_AFTER_WATCH_IN_MS).Forget();
            }

            //when reward ad watch success
            if (_isOnWatchedRewarededSuccessed)
            {
                _isOnWatchedRewarededSuccessed = false;

                _onSuccessWatchVideo?.Invoke();
                _onSuccessWatchVideo = null;
            }
        }

        //private IEnumerator StartRequestAfterSeconds()
        //{
        //    if (AdManager.Instance.IsDeviceWeak)
        //        yield break;
        //    yield return null;

        //    for (int i = 0; i < CurMobileAds.Length; i++)
        //    {
        //        if (CurMobileAds[i]._isWaitingRequestVideo)
        //        {
        //            CurMobileAds[i].RequestNewVideoAfterClosing();
        //        }
        //    }
        //}
        //private void OnFailedLoadedRewardedVideo()
        //{
        //    int indexAdToReload = -1;
        //    for (int i = 0; i < CurMobileAds.Length; i++)
        //    {
        //        if (CurMobileAds[i]._isWaitingRequestVideo && CurMobileAds[i].timeWaitNextReload <= 0)
        //        {
        //            indexAdToReload = i;
        //            break;
        //        }
        //    }
        //    if (indexAdToReload < 0)
        //        return;
        //    float secondToWait = CurMobileAds[indexAdToReload]._failedTimes * MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED + 0.1f;
        //    if (secondToWait >= MAX_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED)
        //    {
        //        secondToWait = MAX_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED;
        //    }
        //    StartCoroutine(RequestNewAdAfterSeconds(secondToWait, indexAdToReload));
        //}

        protected virtual async UniTask RequestNewAdAfterSeconds(int timeInMs)
        {
            await UniTask.Delay(timeInMs);

            RequestNewAd();
        }

        /// <summary>
        /// Goi tu event Google Mobile Ads
        /// </summary>
        protected virtual void HandleRewardedVideoClosed()
        {
            _isOnClosedRewardedVideo = true;
        }
        protected virtual void HandleWatchVideoSuccess()
        {
            _isOnWatchedRewarededSuccessed = true;
        }

        protected virtual void HandleAdOpen()
        {
            _onOpenRewardedVideo?.Invoke();
            _onOpenRewardedVideo = null;
        }

        protected virtual void HandleAdLoadedFailed(string error)
        {
            _isOnFailedVideoLoaded = true;
        }
    }
}