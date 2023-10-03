//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class IronSourceInterstitialAd : InterstitialModel
//{

//    public override bool IsLoaded()
//    {
//        //adReady = IronSource.Agent.isInterstitialReady();
//        return adReady;
//    }

//    public override void Request()
//    {
//        isWaitingToRequest = false;
//        isRequesting = true;
//        adReady = false;

//        CommonLog.LogError("request Ironsource Intersitital");
//        IronSource.Agent.validateIntegration();

//        IronSource.Agent.init(AdConst.IronSourceAdKey, IronSourceAdUnits.INTERSTITIAL);
//        IronSource.Agent.loadInterstitial();
//        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
//        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
//        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
//        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
//        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
//        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
//        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
//    }

//    private void InterstitialAdClosedEvent()
//    {
//        OnAdClose();
//    }

//    private void InterstitialAdOpenedEvent()
//    {
//        OnAdOpen();
//    }

//    private void InterstitialAdClickedEvent()
//    {
//    }

//    private void InterstitialAdShowFailedEvent(IronSourceError error)
//    {

//    }

//    private void InterstitialAdShowSucceededEvent()
//    {
//    }

//    private void InterstitialAdLoadFailedEvent(IronSourceError error)
//    {
//        if (!AdManager.IsAlive)
//            return;
//        OnFailedLoad(error.getErrorCode() + "_" + error.getDescription());
//    }

//    private void InterstitialAdReadyEvent()
//    {
//        OnLoadAdSuccess();
//    }

//    public override bool Show()
//    {
//        if (IronSource.Agent.isInterstitialReady())
//        {
//            CommonLog.Log("IronSourceAdModel ad show");
//            adReady = false;
//            IronSource.Agent.showInterstitial();
//            return true;
//        }
//        CommonLog.LogError("IronSourceAdModel ad can't show");
//        return false;

//    }

//}
