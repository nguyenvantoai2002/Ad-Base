using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolffun.Ads
{
    public class CommonRewardAdController : CommonRewardAdController<AdFactoryBase, RewardedAdModelBase>
    {
        protected override void CreateRewardAdModelBase()
        {
            _rewardedModel = _adFactoryBase.GetCommonRewardedAd();
            SetCallbackToAdModel(_rewardedModel);
        }

    }

    public abstract class CommonRewardAdController<TAdFactory, TRewardAdModel> : RewardedAdControllerBase
        where TAdFactory : AdFactoryBase
        where TRewardAdModel : RewardedAdModelBase
    {
        [SerializeField] protected TAdFactory _adFactoryBase;

        protected TRewardAdModel _rewardedModel;

        public override bool IsLoadedAd()
        {
            return _rewardedModel.IsRewardedVideoLoaded;
        }

        public override void Request()
        {
            _rewardedModel.Request();
        }


        protected override void ShowAd()
        {
            _rewardedModel.Show();
        }
    }
}