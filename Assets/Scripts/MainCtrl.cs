using UnityEngine;
using UnityEngine.UI;

public class MainCtrl : MonoBehaviour
{
    Button webView_btn;
    Button preload_webView_btn;
    Button act_join_btn;
    Button ad_click_btn;

    //广告位id，商务获取
    private string placementId = "q842c2e079a1b32c8";

    // Start is called before the first frame update
    void Start()
    {
        //普通webview触发按钮
        webView_btn = GameObject.Find("webView_btn").GetComponent<Button>();
        //预加载webview触发按钮
        preload_webView_btn = GameObject.Find("preload_webView_btn").GetComponent<Button>();
        //预加载webview触发按钮默认隐藏
        preload_webView_btn.gameObject.SetActive(false);

        act_join_btn = GameObject.Find("act_join_btn").GetComponent<Button>();
        act_join_btn.gameObject.SetActive(false);
        ad_click_btn = GameObject.Find("ad_click_btn").GetComponent<Button>();
        ad_click_btn.gameObject.SetActive(false);

        //初始化成功回调
        AppLuckEvents.onInitSuccessEvent += () =>
        {
            //预加载成功后显示按钮
            preload_webView_btn.gameObject.SetActive(true);
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
            AppLuck.instance.loadPlacement(placementId, "icon", 200, 200);

            AppLuck.instance.loadPlacement("q842c2e0a9a1e19c3", "icon", 200, 200);
        };


        //普通webview触发按钮点击事件绑定
        webView_btn.onClick.AddListener(() =>
        {
            //唤起webview并加载活动，请传入placementId和gaid
            AppLuck.instance.openInteractiveAds("q842c2e0a9a1e19c3");
        });

        //预加载webview触发按钮点击事件绑定
        preload_webView_btn.onClick.AddListener(() =>
        {
            //唤起预加载webview
            AppLuck.instance.openInteractiveAds(placementId);
        });
        //预加载webview初始化
        AppLuck.instance.init(placementId);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
