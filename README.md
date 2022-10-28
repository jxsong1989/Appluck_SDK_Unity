Appluck Android Unity 插件
=========


下载unitypackage
--------
 [AppLuck_UnityPlugin_v1.0.0][alup]

导入unitypackage
--------
1. 在unity中, 选择 Assets > Import Package > Custom Package…
2. 选择下载的AppLuck_UnityPlugin_v1.0.0.unitypackage.
3. 点击导入按钮.

配置依赖
--------
* 通过gradle引入
  ```groovy
  implementation 'androidx.appcompat:appcompat:1.3.0'
  implementation 'com.google.android.material:material:1.4.0'
  //常用工具
  implementation 'org.apache.commons:commons-lang3:3.3.2'
  //http请求
  implementation 'org.jsoup:jsoup:1.11.2'
  //json处理
  implementation 'com.alibaba:fastjson:1.1.72.android'
  //图片加载缓存
  implementation 'com.github.bumptech.glide:glide:4.14.2'
  ```
 * 手动引入
    * 下载libs并解压.
    * 将libs中内容全部放入 Assets/Plugins/Android.
  
要求
--------
* Unity 5.x.x或Unity 2017.x.x 以上.
* 启用Jetifier和AndroidX,在gradleTemplate.properties中添加以下内容:
  ```
  android.useAndroidX=true
  android.enableJetifier=true 
  ```

初始化SDK
--------
1. 填充初始化成功回调. 
  ```c#
  AppLuckEvents.onInitSuccessEvent += () =>{
    //AppLuck SDK 初始化成功,loadPlacement或显示自定义素材
  }
  ```
2. 初始化
  ```c#
  //gaid google ad id
  //placementId 期望预加载的placementId
  AppLuck.instance.init(gaid, placementId);
  ```

loadPlacement(可选)
--------
1. 填充placement加载成功回调
  ```c#
  //sk 加载成功的placement id
  AppLuckEvents.onPlacementLoadSuccessEvent += (sk) =>{
    //placement 加载成功，showPlacement素材
    if (sk == placementId)
    {
        //将placement入口素材显示在指定坐标
        AppLuck.instance.showInteractiveEntrance(sk, Screen.height - 800, Screen.width - 600);
    }
  }
  ```
2. 加载placement素材
  ```c#
  //placementId 广告位id
  //creative type 仅支持 icon
  //素材width 
  //素材height 
  AppLuck.instance.loadPlacement(placementId, "icon", 200, 200);
  ```

自定义placement入口素材(与loadPlacement二选一使用)
--------
```c#
//游戏初始化时默认隐藏入口素材游戏对象placement
placement.gameObject.SetActive(false);

//placement绑定点击事件
placement.onClick.AddListener(() =>
{
    //唤起webview并加载活动，请传入placementId
    AppLuck.instance.openInteractiveAds(请传入placementId);
});

//在SDK初始化成功的回调中显示placement
AppLuckEvents.onInitSuccessEvent += () =>{
    placement.gameObject.SetActive(true);
}
```




 [alup]: https://github.com/jxsong1989/appluck-intergration-guide-uniwebview-unity/releases/tag/v1
