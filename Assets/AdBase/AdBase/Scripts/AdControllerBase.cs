using UnityEngine;

namespace Toga.Ads
{

    public abstract class AdControllerBase : MonoBehaviour
    {
        [SerializeField] protected AdRequestController _adRequestController;

        protected void RequestNewAd()
        {
            _adRequestController.RequestAd(this);
        }

        public abstract void Request();
        public abstract bool IsLoadedAd();
    }
}