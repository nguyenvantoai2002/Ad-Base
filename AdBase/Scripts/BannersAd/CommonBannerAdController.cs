using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolffun.Ads
{

    public class CommonBannerAdController : CommonBannerAdController<AdFactoryBase, BannerAdModelBase>
    {
        protected override void CreateBannerAdModelBase()
        {
            _bannerModel = _adFactoryBase.GetCommonBannerAd();
            SetCallbackToAdModel(_bannerModel);
        }
    }

    public abstract class CommonBannerAdController<TAdFactory, TBannerAdModel> : BannerAdControllerBase 
        where TAdFactory : AdFactoryBase 
        where TBannerAdModel: BannerAdModelBase
    {
        [SerializeField] protected TAdFactory _adFactoryBase;

        protected TBannerAdModel _bannerModel;

        protected override void ShowBanner()
        {
            if (_bannerModel.IsLoaded && !_bannerModel.IsShow)
            {
                _bannerModel.Show();
            }
        }

        protected override void DestroyBanner()
        {
            _bannerModel.Destroy();
        }

        public override void HideBanner()
        {
            if (_bannerModel.IsShow)
            {
                _bannerModel.Hide();
            }
        }

        public override bool IsLoadedAd()
        {
            return _bannerModel != null ? _bannerModel.IsLoaded : false;
        }

        public override void Request()
        {
            _bannerModel.Request();
        }


    }
}