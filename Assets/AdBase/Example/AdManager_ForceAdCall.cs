//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using Wolffun.Log;

//namespace Wolffun.Ads
//{
//    public partial class AdManager
//    {
//        public void ShowForceAd(int adToShow, string reason, Action callback = null)
//        {
//            //TODO: check condition for showing force ad
//            // check last force ads show 5min ago:
//            int forceAdsVal;
//            if (UserData.firstOpenApp)
//            {
//                // first time open app:
//                forceAdsVal = RemoteConfigManager.Instance.FirstAppOpenAdsShowTime;
//                // add callback set first open app failed:
//                callback += () =>
//                {
//                    UserData.firstOpenApp = false;
//                    UserData.saveData();
//                };
//            }
//            else
//            {
//                // not first time open app:
//                forceAdsVal = RemoteConfigManager.Instance.AdsShowTime;
//            }
//            if (Time.time - ParameterPass.lastForceAdsShow < forceAdsVal || UserData.adsRemoved)
//            {
//                return;
//            }
//            //////////////////////////////////////
//            StartCoroutine(RealShowForceAd(callback, reason));
//        }


//        private IEnumerator RealShowForceAd(Action callback, string reason)
//        {
//            if (interstitialAdManager.IsLoadedAd())
//            {
//                //TODO: show loading ad popup
//                if (SceneManager.GetActiveScene().buildIndex == 2)
//                {
//                    UIController.instance.ForceAdsBgOn();
//                }
//                else
//                {
//                    MainSceneController.instance.ForceAdsBGOn();
//                }


//                ////////////////////////////////////

//                yield return new WaitForSeconds(0.5f);

//                //TODO: add the callback with remove the loading ad popup

//                //For ex:
//                //callback += () => inGameState.StopPlayGame(false);

//                callback += () =>
//                {
//                    if (SceneManager.GetActiveScene().buildIndex == 2)
//                    {
//                        UIController.instance.ForceAdsBgOff();
//                    }
//                    else
//                    {
//                        MainSceneController.instance.ForceAdsBGOff();
//                    }
//                    ParameterPass.lastForceAdsShow = Time.time;
//                };

//                ////////////////////////////////////

//                interstitialAdManager._onCloseForceInterstial += callback;
//                interstitialAdManager._onCloseForceInterstial += OnCloseForceAdManager;

//                interstitialAdManager._onFailToShowInterstial += callback;

//                interstitialAdManager._onOpenInterstial += OnOpenForceAdManager;

//                if (!interstitialAdManager.ShowForceAd(1))
//                {
//                    CommonLog.Log("ShowForceAd failed");

//                    //TODO: remove the loading ad popup
//                    if (SceneManager.GetActiveScene().buildIndex == 2)
//                    {
//                        UIController.instance.ForceAdsBgOff();
//                    }
//                    else
//                    {
//                        MainSceneController.instance.ForceAdsBGOff();
//                    }
//                    ////////////////////////////////////

//                    ShowAdFail(callback, AdType.Force);
//                }
//            }
//            else
//            {
//                ShowAdFail(callback, AdType.Force);
//            }
//        }


//        #region Callback for some event

//        void OnCloseForceAdManager()
//        {
//            //do sth when close force ad
//            //Analytic: Impression - Force
//            if (SceneManager.GetActiveScene().buildIndex == 2)
//            {
//                AnalyticManager.Instance.LogAds("Impression", "Force", "End", "NULL");
//            }
//            else
//            {
//                AnalyticManager.Instance.LogAds("Impression", "Force", "Main", "NULL");
//            }

//        }

//        void OnOpenForceAdManager()
//        {
//            //do sth when open force ad

//            //For ex:
//            //requestAd.x++;
//            //loadedAd.x++;
//            //AnalyticManager.Instance.LogAd(AdAction.Trigger, AdType.Force, buttonClick: reason); //Analytic
//            // Analytic: Trigger-Force
//            if (SceneManager.GetActiveScene().buildIndex == 2)
//            {
//                AnalyticManager.Instance.LogAds("Trigger", "Force", "End", "NULL");
//            }
//            else
//            {
//                AnalyticManager.Instance.LogAds("Trigger", "Force", "Main", "NULL");
//            }

//            ////////////////////////////////////////
//        }


//        #endregion
//    }
//}