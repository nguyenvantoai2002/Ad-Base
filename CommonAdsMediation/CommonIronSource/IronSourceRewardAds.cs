//using UnityEngine;

//public class IronSourceRewardAds : BaseRewardedVideo
//{
//    #region Rewarded Video
//    #region Iron Source Event callback
//    private void RewardedVideoAdShowFailedEvent(IronSourceError obj)
//    {
//        HandleRewardBasedVideoFailedToLoad(obj.getErrorCode() + "_" + obj.getDescription());

//    }

//    private void RewardedVideoAdRewardedEvent(IronSourcePlacement obj)
//    {
//        //CommonLog.LogError("RewardedVideoAdRewardedEvent");
//        RewardedAdManager.Instance.HandleWatchVideoSuccess();
//    }

//    private void RewardedVideoAdEndedEvent()
//    {
//        //CommonLog.LogError("RewardedVideoAdEndedEven");
//    }

//    private void RewardedVideoAdStartedEvent()
//    {
//        //CommonLog.LogError("RewardedVideoAdStartedEvent");
//    }

//    private void RewardedVideoAdClosedEvent()
//    {
//        //CommonLog.LogError("RewardedVideoAdClosedEvent");
//        HandleRewardedVideoClosed();
//    }

//    private void RewardedVideoAdOpenedEvent()
//    {
//        //CommonLog.LogError("RewardedVideoAdOpenedEvent");    
//        HandleRewardedVideoOpened();
//    }

//    private void RewardedVideoAdClickedEvent(IronSourcePlacement obj)
//    {
//    }

//    private void RewardedVideoAvailabilityChangedEvent(bool isAvaiable)
//    {
//        if (IsRewardedVideoLoaded != isAvaiable && isAvaiable)
//        {
//            HandleRewardedVideoLoaded();
//        }
//        IsRewardedVideoLoaded = isAvaiable;
//    }

//    #endregion



//    public override void UserOptToWatchAd()
//    {
//        if (IsRewardedVideoLoaded)
//        {
//            //LoadingScene.Instance.LockScreen(true);
//            IronSource.Agent.showRewardedVideo("Free_Rewards");
//        }
//        else
//        {
//            CommonLog.LogError("UserOptToWatchAd but not loaded");
//        }
//    }

//    public int GetRewardedAdFailTimes()
//    {
//        return 1;
//    }

//    public override void InitRewardedVideo(GameObject gameObject = null)
//    {
//        IronSource.Agent.init(AdConst.IronSourceAdKey, IronSourceAdUnits.REWARDED_VIDEO);
//        IronSource.Agent.shouldTrackNetworkState(true);
//        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
//        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
//        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
//        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
//        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
//        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
//        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
//        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
//        //IronSource.Agent.validateIntegration();
//    }
//    #endregion

//}