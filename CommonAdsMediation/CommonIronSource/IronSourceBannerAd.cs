//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class IronSourceBannerAd : BannerAdModel
//{
//    public override void Request()
//    {
//        IsWaitingToRequest = false;
//        isRequesting = true;

//        CommonLog.LogError("request Ironsource BannerAd");
//        IronSource.Agent.validateIntegration();

//        IronSource.Agent.init(AdConst.IronSourceAdKey, IronSourceAdUnits.BANNER);
//        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);

//        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
//        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
//        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
//        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
//        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
//        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
//    }

//    private void BannerAdLeftApplicationEvent()
//    {
//    }

//    private void BannerAdScreenDismissedEvent()
//    {
//    }

//    private void BannerAdScreenPresentedEvent()
//    {
//    }

//    private void BannerAdClickedEvent()
//    {
//    }

//    private void BannerAdLoadFailedEvent(IronSourceError obj)
//    {
//        CommonLog.LogError("BannerAd Load fail: " + obj.getDescription());
//        AdLoadFail(obj.getErrorCode() + "_" + obj.getDescription());

//    }

//    private void BannerAdLoadedEvent()
//    {
//        CommonLog.LogError("BannerAd Loaded");
//        AdLoaded();
//        Show();
//    }

//    public override void Show()
//    {
//        IronSource.Agent.displayBanner();
//    }

//    public override void Destroy()
//    {
//        IronSource.Agent.destroyBanner();
//    }
//}
