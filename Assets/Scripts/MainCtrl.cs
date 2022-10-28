using UnityEngine;
using UnityEngine.UI;

public class MainCtrl : MonoBehaviour
{
    Button webView_btn;
    Button preload_webView_btn;
    Button act_join_btn;
    Button ad_click_btn;

    //���λid�������ȡ
    private string placementId = "q842c2e079a1b32c8";
    //gaid��ͨ��google sdk��ȡ
    private string gaid = "";
    // Start is called before the first frame update
    void Start()
    {
        //��ͨwebview������ť
        webView_btn = GameObject.Find("webView_btn").GetComponent<Button>();
        //Ԥ����webview������ť
        preload_webView_btn = GameObject.Find("preload_webView_btn").GetComponent<Button>();
        //Ԥ����webview������ťĬ������
        preload_webView_btn.gameObject.SetActive(false);

        act_join_btn = GameObject.Find("act_join_btn").GetComponent<Button>();
        act_join_btn.gameObject.SetActive(false);
        ad_click_btn = GameObject.Find("ad_click_btn").GetComponent<Button>();
        ad_click_btn.gameObject.SetActive(false);
        gaid = GetAdvertisingId();
        Debug.LogError("GetAdvertisingId  " + gaid);

        //��ʼ���ɹ��ص�
        AppLuckEvents.onInitSuccessEvent += () =>
        {
            //Ԥ���سɹ�����ʾ��ť
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


        //��ͨwebview������ť����¼���
        webView_btn.onClick.AddListener(() =>
        {
            //����webview�����ػ���봫��placementId��gaid
            AppLuck.instance.openInteractiveAds("q842c2e0a9a1e19c3");
        });

        //Ԥ����webview������ť����¼���
        preload_webView_btn.onClick.AddListener(() =>
        {
            //����Ԥ����webview
            AppLuck.instance.openInteractiveAds(placementId);
        });
        //Ԥ����webview��ʼ��
        AppLuck.instance.init(gaid, placementId);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationFocus(bool focus)
    {
        //ʧȥ�������loadPlacement��ʵ���л�icon�����л�����ɺ���
        if (!focus)
        {
            AppLuck.instance.loadPlacement(placementId, "icon", 200, 200);

            AppLuck.instance.loadPlacement("q842c2e0a9a1e19c3", "icon", 200, 200);
        }
    }

    private string GetAdvertisingId()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass jc2 = new AndroidJavaClass("com.google.android.gms.ads.identifier.AdvertisingIdClient");
        AndroidJavaObject jo2 = jc2.CallStatic<AndroidJavaObject>("getAdvertisingIdInfo", jo);
        if (jo2 != null)
        {
            //��ȡ���id��
            string advertisingId = jo2.Call<string>("getId");
            if (!string.IsNullOrEmpty(advertisingId))
            {
                return advertisingId;
            }
        }
        return "";
    }
}
