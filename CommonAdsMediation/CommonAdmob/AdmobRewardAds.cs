#if AD_MOB
using GoogleMobileAds.Api;
//using GoogleMobileAds.Api.Mediation.AdColony;
using System;

public class AdmobRewardAds : BaseRewardedVideo
{
    //private RewardBasedVideoAd rewardBasedVideo;

    private RewardedAd rewardBasedVideo;

    public override void Request()
    {
        IsWaitingRequestVideo = IsRewardedVideoLoaded = false;
        isRequestingRewardedVideo = true;

        if (AdConst.IsAdMobSDKInit)
            RealRequest();
        
    }

    private void RealRequest()
    {
        rewardBasedVideo = new RewardedAd(AdConst.AdmobRewardedID_type0);

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        //aCreate an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        HandleRewardedVideoLoaded();
    }


#if !NO_PLUGIN
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //Lai doi co che moi
        CommonLog.LogError("RewardedAd Load fail: " + args.LoadAdError.ToString());

        HandleRewardBasedVideoFailedToLoad(args.LoadAdError.GetCode().ToString());
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        HandleRewardedVideoOpened();
        //UserStatusDataManager.Instance.IsServicePause = true;
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        HandleRewardedVideoClosed();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {

        //string type = args.Type;
       
        HandleOnAdRewarded();
        
    }


    public override void UserOptToWatchAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();

        }
    }
#endif

}

#endif