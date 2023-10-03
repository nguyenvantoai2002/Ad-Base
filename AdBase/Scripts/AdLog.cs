using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Wolffun.Log;

namespace Wolffun.Ads
{
    public static class AdLog
    {
        private const string AD_LOG_STR_FORMAT = "AdService: {0}";

        public static string GetLogString(object log)
        {
            return string.Format(AD_LOG_STR_FORMAT, log);
        }
    }
}