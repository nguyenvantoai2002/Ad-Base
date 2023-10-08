using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toga.Ads
{
    [System.Serializable]
    public enum AdOperationSystem
    {
        Editor = 0,
        Android = 1,
        iOS = 2,
    }

    public static class AdOperationSystemChecker
    {
        public static AdOperationSystem GetAdOperationSystem()
        {
#if UNITY_EDITOR
            return AdOperationSystem.Editor;
#elif UNITY_ANDROID
            return AdOperationSystem.Android;
#elif UNITY_IOS
            return AdOperationSystem.iOS;
#else
            return AdOperationSystem.Editor;
#endif
        }
    }
}