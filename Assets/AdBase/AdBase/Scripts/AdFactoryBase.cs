using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toga.Ads
{
    public abstract class AdFactoryBase : ScriptableObject
    {
        public abstract UniTask<bool> InitSDK();

        public virtual InterstitialModelBase GetCommonForceAd()
        {
            return null;
        }

        public virtual RewardedAdModelBase GetCommonRewardedAd()
        {
            return null;
        }

        public virtual BannerAdModelBase GetCommonBannerAd()
        {
            return null;
        }
    }
}