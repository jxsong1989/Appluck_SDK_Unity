using UnityEngine;
using UnityEngine.UI;

public class MainCtrl : MonoBehaviour
{
    Button webView_btn;
    Button preload_webView_btn;
    Button webView_btn2;
    Button preload_webView_btn2;
    Button act_join_btn;
    Button ad_click_btn;
    Button open_url_btn;
    Button placement_hide_btn;
    Button placement_show_btn;


    //广告位id，商务获取
    private string placementId = "q842c2e079a1b32c8";

    // Start is called before the first frame update
    void Start()
    {
        //普通webview触发按钮
        webView_btn = GameObject.Find("webView_btn").GetComponent<Button>();
        //普通webview触发按钮
        webView_btn2 = GameObject.Find("webView_btn2").GetComponent<Button>();
        //预加载webview触发按钮
        preload_webView_btn = GameObject.Find("preload_webView_btn").GetComponent<Button>();
        //预加载webview触发按钮默认隐藏
        preload_webView_btn.gameObject.SetActive(false);

        preload_webView_btn2 = GameObject.Find("preload_webView_btn2").GetComponent<Button>();
        //预加载webview触发按钮默认隐藏
        preload_webView_btn2.gameObject.SetActive(false);

        act_join_btn = GameObject.Find("act_join_btn").GetComponent<Button>();
        act_join_btn.gameObject.SetActive(false);
        ad_click_btn = GameObject.Find("ad_click_btn").GetComponent<Button>();
        ad_click_btn.gameObject.SetActive(false);

        open_url_btn = GameObject.Find("open_url_btn").GetComponent<Button>();
        open_url_btn.onClick.AddListener(() =>
        {
            AppLuck.instance.openUrl("https://aios.soinluck.com/scene?sk=q842c2e079a1b32c8&lzdid={gaid}", 2, 1);
        });

        placement_hide_btn = GameObject.Find("placement_hide_btn").GetComponent<Button>();
        placement_hide_btn.onClick.AddListener(() =>
        {
            AppLuck.instance.hideInteractiveEntrance(placementId); ;
        });

        placement_show_btn = GameObject.Find("placement_show_btn").GetComponent<Button>();
        placement_show_btn.onClick.AddListener(() =>
        {
            AppLuck.instance.showInteractiveEntrance(placementId, Screen.height - 800, Screen.width - 300);
        });

        //初始化成功回调
        AppLuckEvents.onInitSuccessEvent += () =>
        {
            toast("AppLuck Init Success.");
            //预加载成功后显示按钮
            preload_webView_btn.gameObject.SetActive(true);
            preload_webView_btn2.gameObject.SetActive(true);
            AppLuckEvents.onPlacementLoadSuccessEvent += (sk) =>
            {
                if (sk == "q842c2e0a9a1e19c3")
                {
                    AppLuck.instance.showInteractiveEntrance(sk, Screen.height - 800, Screen.width - 600);
                }
                else if (sk == placementId)
                {
                    AppLuck.instance.showInteractiveEntrance(sk, Screen.height - 800, Screen.width - 300);
                }
            };
            AppLuckEvents.onUserInteractionEvent += (placementId, interaction) =>
            {
                toast(placementId + "  " + interaction);
            };
            AppLuck.instance.loadPlacement(placementId, "icon", 200, 200);

            AppLuck.instance.loadPlacement("q842c2e0a9a1e19c3", "icon", 200, 200);
        };


        //普通webview触发按钮点击事件绑定
        webView_btn.onClick.AddListener(() =>
        {
            AppLuckEvents.onInteractiveAdsHidden += (placementId, status) =>
            {
                Debug.Log("puutiiiiiiii onInteractiveAdsHidden: " + placementId + ", " + status);
                toast("onInteractiveAdsHidden: " + placementId + ", " + status);
            };
            //唤起webview并加载活动，请传入placementId和gaid
            AppLuck.instance.openInteractiveAds("q842c2e0a9a1e19c3", 1);
        });

        //普通webview触发按钮点击事件绑定
        webView_btn2.onClick.AddListener(() =>
        {
            AppLuckEvents.onInteractiveAdsHidden += (placementId, status) =>
            {
                Debug.Log("puutiiiiiiii onInteractiveAdsHidden: " + placementId + ", " + status);
                toast("onInteractiveAdsHidden: " + placementId + ", " + status);
            };
            //唤起webview并加载活动，请传入placementId和gaid
            AppLuck.instance.openInteractiveAds("q842c2e0a9a1e19c3", 2, 2);
        });

        //预加载webview触发按钮点击事件绑定
        preload_webView_btn.onClick.AddListener(() =>
        {
            AppLuckEvents.onInteractiveAdsHidden += (placementId, status) =>
            {
                Debug.Log("puutiiiiiiii onInteractiveAdsHidden: " + placementId + ", " + status);
                toast("onInteractiveAdsHidden: " + placementId + ", " + status);
            };
            //唤起预加载webview
            AppLuck.instance.openInteractiveAds(placementId, 2, 2);
        });

        //预加载webview触发按钮点击事件绑定
        preload_webView_btn2.onClick.AddListener(() =>
        {
            AppLuckEvents.onInteractiveAdsHidden += (placementId, status) =>
            {
                Debug.Log("puutiiiiiiii onInteractiveAdsHidden: " + placementId + ", " + status);
                toast("onInteractiveAdsHidden: " + placementId + ", " + status);
            };
            //唤起预加载webview
            AppLuck.instance.openInteractiveAds(placementId, 1);
        });
        //预加载webview初始化
        AppLuck.instance.init(placementId);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void toast(string msg)
    {
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", msg);
            Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT")).Call("show");
        }
        ));
    }
}
