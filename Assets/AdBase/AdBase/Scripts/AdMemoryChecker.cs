using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolffun.Log;

namespace Wolffun.Ads
{
    public static class AdMemoryChecker
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        private static AndroidJavaObject _currentActivity;
        private static AndroidJavaClass _apiClass;
        private static AndroidJavaObject _apiInstance;

#endif
        public static Action s_OnLowMemory;

        public static bool IsDeviceWeak()
        {
#if UNITY_IOS
            return false;
#else
            int memory = SystemInfo.systemMemorySize;
            bool result = memory <= 1024 && CheckLowFreeMemoryOnNativeFunction();
            if (result)
                s_OnLowMemory?.Invoke();
            return result;
#endif
        }

        private static bool CheckLowFreeMemoryOnNativeFunction()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            if (_apiInstance == null)
            {
                if (null == _apiClass)
                    _apiClass = new AndroidJavaClass("com.zp.utility.api");

                _apiInstance = _apiClass.CallStatic<AndroidJavaObject>("instance");
            }

            if (null == _currentActivity)
                _currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("_currentActivity");

            if (_apiInstance != null && _currentActivity != null)
            {
                var remainMemory = _apiInstance.Call<int>("GetAndroidRemianMemory", _currentActivity);
                CommonLog.Log(AdLog.GetLogString("remainMemory: " + remainMemory));

                return (remainMemory) <= 250;
            }
            else
            {
                CommonLog.Log(AdLog.GetLogString("remainMemory nullllll"));
            }

            #endif
            return false;
        }
    }
}