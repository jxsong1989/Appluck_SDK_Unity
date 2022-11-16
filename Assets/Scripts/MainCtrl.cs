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

        //��ʼ���ɹ��ص�
        AppLuckEvents.onInitSuccessEvent += () =>
        {
            toast("AppLuck Init Success.");
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
            AppLuckEvents.onUserInteractionEvent += (placementId, interaction) =>
            {
                toast(placementId + "  " + interaction);
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
