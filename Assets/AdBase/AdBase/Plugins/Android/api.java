package com.zp.utility;

import android.app.Activity;
import android.app.ActivityManager;
import android.content.Context;
public class api {


    private static api _instance;
    public static api instance()
    {
        if(null == _instance)
            _instance = new api();
        return _instance;
    }

    public int GetAndroidRemianMemory(Object object)
    {
        int remainMemory = -1;
        Activity activity = (Activity)object;
        if(activity != null) {
            ActivityManager activityManager = (ActivityManager)activity.getSystemService(Context.ACTIVITY_SERVICE);
            ActivityManager.MemoryInfo memoryInfo = new ActivityManager.MemoryInfo();
            activityManager.getMemoryInfo(memoryInfo);
            remainMemory = (int)(memoryInfo.availMem / 1024 / 1024);
            //Log.e("ZP", "memoryInfo.totalMem: " + memoryInfo.totalMem + "--memoryInfo.availMem: " + memoryInfo.availMem + "--memoryInfo.lowMemory: " + memoryInfo.lowMemory);
        }
        return remainMemory;
    }
}
