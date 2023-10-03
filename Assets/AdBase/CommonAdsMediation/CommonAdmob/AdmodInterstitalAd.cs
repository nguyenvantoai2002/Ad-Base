#if AD_MOB
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmodInterstitalAd : InterstitialModel
{
    public InterstitialAd curInterial;
    public override bool IsLoaded()
    {
        return adReady;
    }

    public override void Request()
    {
        isWaitingToRequest = false;
        adReady = false;
        isRequesting = true;
        if (curInterial != null)
        {
            curInterial.Destroy();
        }
        if (AdConst.IsAdMobSDKInit)
            RealRequest();
        //else AdConst.InitAdMobSDK((status) =>
        //{
        //    RealRequest();
        //});
    }

    private void RealRequest()
    {
        // Initialize an InterstitialAd.
        curInterial = new InterstitialAd(AdConst.AdmobInterstitialID_type0);
        // Called when an ad request has successfully loaded.
        curInterial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        curInterial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        curInterial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.

        curInterial.OnAdClosed += HandleOnAdClosed;
        //// Called when the ad click caused the user to leave the application.
        //this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .Build();

        // Load the interstitial with the request.
        curInterial.LoadAd(request);
    }

    private void HandleOnAdOpened(object sender, EventArgs e)
    {
        OnAdOpen();
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        OnAdClose();
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if (!AdManager.IsAlive)
            return;

        CommonLog.LogError("InterstitialAd Load fail: " + e.LoadAdError.ToString());

        OnFailedLoad(e.LoadAdError.GetCode().ToString());
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        OnLoadAdSuccess();
    }

    public override bool Show()
    {
        this.adReady = false;
        if (curInterial == null)
            return false;
        if (curInterial.IsLoaded())
        {
            curInterial.Show();
            return true;
        }
        return false;
    }
}
#endif