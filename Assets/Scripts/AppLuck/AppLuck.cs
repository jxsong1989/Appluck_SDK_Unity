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

    public void init(string gaid, string preloadSk)
    {
        if (preloadSk == null || preloadSk.Length <= 0)
        {
            Debug.LogError("preloadSk is empty");
            return;
        }
        if (gaid == null)
        {
            gaid = "";
        }
        appLuckSdk.CallStatic("init", preloadSk, gaid);
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

    public void openInteractiveAds(string sk)
    {
        appLuckSdk.CallStatic("openInteractiveAds", sk);
    }
}

