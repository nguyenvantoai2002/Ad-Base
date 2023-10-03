//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//namespace Wolffun.Ads
//{
//    public partial class AdManager
//    {
//        public void ShowRewardedVideo(Action OnSuccessRewardedVideo, AdSourceCalled adSourceCalled, Action OnCloseRewardVideo = null)
//        {
//            if (IsDeviceWeak)
//            {
//                //Show ad error if device is weak

//                //For ex:
//                //PopupManager.instance.ShowPopup<AdErrorPopup>(PUType.AD_ERROR);
//                if (SceneManager.GetActiveScene().buildIndex == 2)
//                {
//                    // Show Ads Loading Failed ingame
//                    UIController.instance.AdsLoadingFailedOn();
//                }
//                else
//                {
//                    // Show Ads Loading Failed Map Menu
//                }

//                ////////////////////////////////////////////
//                return;
//            }

//            if (rewardedAdManager.IsRewardedVideoReady())
//            {
//                rewardedAdManager._onCloseRewardedVideo = OnCloseRewardVideo;

//                rewardedAdManager._onSuccessWatchVideo = OnSuccessRewardedVideo;
//                rewardedAdManager._onSuccessWatchVideo += OnWatchRewardSuccessManager;

//                rewardedAdManager._onOpenRewardedVideo += OnOpenRewardManager;


//                rewardedAdManager.StartShowAd();
//            }
//            else
//            {
//                //TODO: show ad error popup

//                //For ex:
//                //if (Application.internetReachability != NetworkReachability.NotReachable)
//                //    requestAd.y++; //Analytic

//                //PopupManager.instance.ShowPopup<AdErrorPopup>(PUType.AD_ERROR);

//                if (SceneManager.GetActiveScene().buildIndex == 2)
//                {
//                    // Show Ads Loading Failed ingame
//                    UIController.instance.AdsLoadingFailedOn();
//                }
//                else
//                {
//                    // Show Ads Loading Failed Map Menu

//                }

//                ////////////////////////////////////////////
//            }
//        }

//        #region Callback for some event

//        void OnWatchRewardSuccessManager()
//        {
//            //do sth when watch rewarded success

//            //For ex:
//            //AnalyticManager.Instance.LogAd(AdAction.Impression, AdType.Reward, buttonClick: adSourceCalled.ToString()); //Analytic
//            // Analytic Impression-Reward:
//            AnalyticManager.Instance.LogAds("Impression", "Reward", "Revive", "Revive");


//            ////////////////////////////////////////////
//        }

//        void OnOpenRewardManager()
//        {
//            //do sth when open rewarded success

//            //For ex:
//            //requestAd.y++;
//            //loadedAd.y++;
//            //AnalyticManager.Instance.LogAd(AdAction.Trigger, AdType.Reward, buttonClick: adSourceCalled.ToString()); //Analytic
//            //Analytic Trigger-Reward
//            AnalyticManager.Instance.LogAds("Trigger", "Reward", "Revive", "Revive");
//            ////////////////////////////////////////////
//        }


//        #endregion

//    }
//}