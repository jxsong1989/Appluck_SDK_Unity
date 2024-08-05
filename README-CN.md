Appluck Android Unity 插件集成说明
=========
[English](https://github.com/jxsong1989/Appluck_SDK_Unity/blob/master/README.md)
<br/>

[Github地址](https://github.com/jxsong1989/Appluck_SDK_Unity)
<br/>


使用要求
--------
Unity 5.x.x或Unity 2017.x.x 以上.

## 1.下载Appluck UnityPlugin
 [AppLuck_UnityPlugin_v1.2.4][alup]

## 2. 导入unitypackage
1. 在unity中, 选择 Assets > Import Package > Custom Package…
2. 选择下载的AppLuck_UnityPlugin_v1.2.0.unitypackage
3. 点击导入按钮

## 3. 配置依赖
可通过以下两种方式引入，任选其一

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
  //gaid
  implementation 'com.google.android.gms:play-services-ads-identifier:18.0.1'
  ```

 * 手动引入

   * 将libs.zip解压，内容全部放入 Assets/Plugins/Android

启用Jetifier和AndroidX,在gradleTemplate.properties中添加以下内容:
  ```
  android.useAndroidX=true
  android.enableJetifier=true 
  ```

## 4. 开始集成

### 4.1 初始化SDK

4.1.1 填充初始化成功回调.

  ```c#
AppLuckEvents.onInitSuccessEvent += () =>{
  //Appluck SDK 初始化成功
  //可以开始设置广告位入口
}
  ```

4.1.2 初始化

  ```c#
//placementId - 广告位ID 插件会自动对该位置做预加载，如产品中有多个广告位建议传入最重要即预期曝光最多的广告位ID。
//生产环境的placementId请与运营人员联系获取。
AppLuck.instance.init(placementId);
  ```

### 4.2 设置广告位入口

Appluck支持两种方式的广告位入口

- 使用封装好的方法加载入口(建议使用)
  - 只需传入入口的宽高及位置即可展示，Appluck将会对素材做系统推荐并根据点击率实时优化
- 自行设置入口(适合对入口有特殊要求，或在某些场景希望直接打开互动广告的需求)
  - 希望打开互动广告时，调用我们提供的方法打开Appluck的活动页面。

#### 4.2.1 使用Appluck封装的入口

1. 填充placement加载成功回调，展示placement

  ```c#
//loadedPlacementId - 加载成功的placementId
AppLuckEvents.onPlacementLoadSuccessEvent += (loadedPlacementId) =>{
  //placement 加载成功，showPlacement素材
  if (loadedPlacementId == placementId)
  {
      //将placement入口素材显示在指定坐标
      AppLuck.instance.showInteractiveEntrance(loadedPlacementId, Screen.height - 800, Screen.width - 600);
  }
}
  ```

2. 加载placement素材

  ```c#
//placementId - 广告位id
//creative type - 素材类型，当前仅支持 icon
//width - 入口位置的素材宽度
//height - 入口位置的素材高度
AppLuck.instance.loadPlacement(placementId, "icon", 200, 200);
  ```

3. 隐藏placement

 ```c#
//placementId - 广告位id
AppLuck.instance.hideInteractiveEntrance(placementId);
  ```


#### 4.2.2 自行设置入口

- 直接打开互动广告的场景请直接调用

```c#
//唤起webview并加载活动，请传入placementId
//mode 
//-- 0.默认模式: 适合固定入口场景如浮标banner等，用户可以自由关闭互动广告界面。
//-- 1.插屏模式: 适合插屏场景，用户进入10秒后才可关闭。
//-- 2.激励模式: 适合激励场景，用户完成1次活动参与后可关闭互动广告界面，关闭界面时触发激励回调。
AppLuck.instance.openInteractiveAds(请传入placementId, mode);

//唤起webview并加载活动，请传入placementId
//mode 
//-- 0.默认模式: 适合固定入口场景如浮标banner等，用户可以自由关闭互动广告界面。
//-- 1.插屏模式: 适合插屏场景，用户进入10秒后才可关闭。
//-- 2.激励模式: 适合激励场景，用户完成{times}次活动参与后可关闭互动广告界面，关闭界面时触发激励回调。
//times
//-- 当mode为2(激励模式)时用于限制用户需要完成的活动参与次数。
AppLuck.instance.openInteractiveAds(请传入placementId, mode, times);
```

- 自行设置入口，等待Appluck预加载完成再展示

```c#
//游戏初始化时默认隐藏入口素材游戏对象placement
placement.gameObject.SetActive(false);

//placement绑定点击事件
placement.onClick.AddListener(() =>
{
    //唤起webview并加载活动，请传入placementId
    //mode 
    //-- 0.默认模式: 适合固定入口场景如浮标banner等，用户可以自由关闭互动广告界面。
    //-- 1.插屏模式: 适合插屏场景，用户进入10秒后才可关闭。
    //-- 2.激励模式: 适合激励场景，用户完成1次活动参与后可关闭互动广告界面，关闭界面时触发激励回调。
    AppLuck.instance.openInteractiveAds(请传入placementId, mode);
});

//在SDK初始化成功的回调中显示placement
AppLuckEvents.onInitSuccessEvent += () =>{
    placement.gameObject.SetActive(true);
}
```

### 4.3 其他事件
```c#
//AppLuck 关闭回调
//status 0:普通关闭;1:已完成激励任务
AppLuckEvents.onInteractiveAdsHidden += (placementId, status) => {
	toast("onInteractiveAdsHidden: " + placementId + ", " + status);
};
```

[alup]: https://github.com/jxsong1989/Appluck_SDK_Unity/releases/tag/v1.2.4
