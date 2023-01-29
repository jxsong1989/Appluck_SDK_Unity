using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AppLuck
{
    private static AppLuck _appLuck;

    private static AndroidJavaObject appLuckSdk;

    private AppLuck()
    {
        appLuckSdk = new AndroidJavaObject("com.appluck.webview_library.AppLuck");
        var type = typeof(AppLuckEvents);
        var mgr = new GameObject("AppLuckEvents", type).GetComponent<AppLuckEvents>();
    }

    public static AppLuck instance
    {
        get
        {
            if (_appLuck == null)
            {
                _appLuck = new AppLuck();
            }
            return _appLuck;
        }
    }

    public void init(string preloadSk)
    {
        if (preloadSk == null || preloadSk.Length <= 0)
        {
            Debug.LogError("preloadSk is empty");
            return;
        }
        appLuckSdk.CallStatic("init", preloadSk);
    }

    public void loadPlacement(string sk, string creativeType, int width, int height)
    {
        if (sk == null || sk.Length <= 0)
        {
            Debug.LogError("preloadSk is empty");
            return;
        }
        appLuckSdk.CallStatic("loadPlacement", sk, creativeType, width, height);
    }

    public void showInteractiveEntrance(string sk, float top, float left)
    {
        if (sk == null || sk.Length <= 0)
        {
            Debug.LogError("preloadSk is empty");
            return;
        }
        appLuckSdk.CallStatic("showInteractiveEntrance", sk, top, left);
    }

    public void hideInteractiveEntrance(string sk) {
        if (sk == null || sk.Length <= 0)
        {
            Debug.LogError("preloadSk is empty");
            return;
        }
        appLuckSdk.CallStatic("hideInteractiveEntrance", sk);
    }

    public void openInteractiveAds(string sk, int mode, int times)
    {
        appLuckSdk.CallStatic("openInteractiveAds", sk, mode, times);
    }

    public void openInteractiveAds(string sk, int mode)
    {
        appLuckSdk.CallStatic("openInteractiveAds", sk, mode, 1);
    }

    public void openUrl(string url, int mode, int times)
    {
        appLuckSdk.CallStatic("openUrl", url, mode, times);
    }

    public bool isPlacementReady(string sk)
    {
        return appLuckSdk.CallStatic<bool>("isPlacementReady", sk);
    }

    public bool isSDKInit()
    {
        return appLuckSdk.CallStatic<bool>("isSDKInit");
    }
}

