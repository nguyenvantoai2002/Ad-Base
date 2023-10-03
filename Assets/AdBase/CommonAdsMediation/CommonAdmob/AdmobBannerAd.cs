#if AD_MOB
using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AdmobBannerAd : BannerAdModel
{
    //bool isInit = false;
    BannerView bannerView;

    public AdmobBannerAd()
    {
    }

    private void HandleInitCompleteAction(InitializationStatus obj)
    {
    }

    public override void Destroy()
    {
        bannerView?.Destroy();
        bannerView = null;
    }

    public override void Request()
    {
        IsWaitingToRequest = false;
       
        // Clean up banner before reusing
        if (bannerView == null)
        {
            bannerView = new BannerView(AdConst.AdmobBannerID_type0, AdSize.SmartBanner, AdPosition.Bottom);

            // Add Event Handlers
            bannerView.OnAdLoaded += OnAdLoaded;
            bannerView.OnAdFailedToLoad += OnAdFailedToLoad;
            bannerView.OnAdOpening += OnAdOpening;
            bannerView.OnAdClosed += OnAdClosed;
            //bannerView.Destroy();
        }

        if (AdConst.IsAdMobSDKInit)
            RealRequest();
    }

    private void RealRequest()
    {
        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());
    }

    private void OnAdLoaded(object sender, EventArgs e)
    {
        CommonLog.LogError("BannerAd Loaded");
        bannerView.Hide();
        AdLoaded();
    }

    private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        //Ad failed to failed
        CommonLog.LogError("BannerAd Load fail: " + e.LoadAdError.ToString());
        AdLoadFail(e.LoadAdError.GetCode().ToString());
        
    }

    private void OnAdOpening(object sender, EventArgs e)
    {

    }

    private void OnAdClosed(object sender, EventArgs e)
    {

    }

    public override void Show()
    {
        bannerView?.Show();
        AdShow();
    }

    public override void Hide()
    {
        bannerView?.Hide();
        AdHide();
    }


    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .Build();
    }

    
}
#endif