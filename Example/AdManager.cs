//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using UnityEngine;
//using Wolffun.Log;
//using Wolffun.Singleton;

//public enum AdType
//{
//    Force,
//    Reward,
//    Banner
//}

//public enum AdAction
//{
//    Trigger,
//    Impression,
//    Claim
//}

//public enum AdNetwork
//{
//    //For ex:
//    Admob,
//    Ironsource
//}

//public enum AdSourceCalled
//{
//    //For ex:
//    None,
//    NewBestPU,
//    StarBoxPU,
//    OutOfRotate,
//    ShopPU
//}

//namespace Wolffun.Ads
//{

//    public partial class AdManager : MonoSingleton<AdManager>
//    {
//        [SerializeField] InterstitialAdControllerBase interstitialAdManager;
//        [SerializeField] RewardedAdControllerBase rewardedAdManager;
//        [SerializeField] BannerAdControllerBase bannerAdManager;

//        protected override void Awake()
//        {
//            base.Awake();
//            DontDestroyOnLoad(gameObject);
//        }

//    }
//}