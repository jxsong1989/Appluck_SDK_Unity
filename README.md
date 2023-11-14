# Appluck Android Unity Plugin Integration Guide

[中文](https://github.com/jxsong1989/Appluck_SDK_Unity/blob/master/README-CN.md)
<br/>
<br/>
[GitHub Repository](https://github.com/jxsong1989/Appluck_SDK_Unity)
<br/>

## System Requirements
Unity 5.x.x or Unity 2017.x.x and above.

## 1. Download Appluck Unity Plugin
[AppLuck_UnityPlugin_v1.2.3](https://github.com/jxsong1989/Appluck_SDK_Unity/releases/tag/v1.2.3)

## 2. Import UnityPackage
1. In Unity, select **Assets > Import Package > Custom Package…**
2. Choose the downloaded `AppLuck_UnityPlugin_v1.2.3.unitypackage`
3. Click the Import button

## 3. Configure Dependencies
You can include dependencies in one of the following ways:

* Using Gradle

  ```groovy
  implementation 'androidx.appcompat:appcompat:1.3.0'
  implementation 'com.google.android.material:material:1.4.0'
  // Common utilities
  implementation 'org.apache.commons:commons-lang3:3.3.2'
  // HTTP requests
  implementation 'org.jsoup:jsoup:1.11.2'
  // JSON processing
  implementation 'com.alibaba:fastjson:1.1.72.android'
  // Image loading and caching
  implementation 'com.github.bumptech.glide:glide:4.14.2'
  // Google Ad ID
  implementation 'com.google.android.gms:play-services-ads-identifier:18.0.1'
  ```
* Manual Integration

   * Unzip libs.zip and place all contents into Assets/Plugins/Android.

Enable Jetifier and AndroidX by adding the following lines to gradleTemplate.properties:
  ```properties
  android.useAndroidX=true
  android.enableJetifier=true 
  ```

## 4. Start Integration

### 4.1 Initialize SDK

#### 4.1.1 Fill in the Initialization Success Callback.

```c#
AppLuckEvents.onInitSuccessEvent += () =>{
  // Appluck SDK initialization successful
  // You can now begin setting up ad placements.
}
```
### 4.2 Set up Ad Placement Entrance

Appluck supports two methods of ad placement entrance:

- Use the encapsulated method to load the entrance (recommended)
  - Simply provide the width, height, and position of the entrance for display. Appluck will recommend materials based on the system and optimize them in real-time based on click-through rates.
- Set up the entrance manually (suitable for cases where there are special requirements for the entrance, or in some scenarios where you want to directly open interactive ads)

#### 4.2.1 Use Appluck's Encapsulated Entrance

1. Fill in the placement loading success callback and display the placement.

  ```c#
//loadedPlacementId - Successfully loaded placementId
AppLuckEvents.onPlacementLoadSuccessEvent += (loadedPlacementId) =>{
  //Placement loaded successfully, showPlacement material
  if (loadedPlacementId == placementId)
  {
      //Display the placement entrance material at the specified coordinates
      AppLuck.instance.showInteractiveEntrance(loadedPlacementId, Screen.height - 800, Screen.width - 600);
  }
}
```

2. Load Placement Material

  ```c#
//placementId - Ad placement id
//creative type - Material type, currently only supports icon
//width - Material width at the entrance position
//height - Material height at the entrance position
AppLuck.instance.loadPlacement(placementId, "icon", 200, 200);
```

3. Hide Placement

 ```c#
//placementId - Ad placement id
AppLuck.instance.hideInteractiveEntrance(placementId);

#### 4.2.2 Set up Entrance Manually

- For scenes directly opening interactive ads, please call the following:

```c#
// Invoke the webview and load the activity, please pass in placementId
// mode 
//-- 0. Default mode: Suitable for fixed entrance scenes such as floating banner, users can freely close the interactive ad interface.
//-- 1. Interstitial mode: Suitable for interstitial scenes, users can close it after 10 seconds.
//-- 2. Reward mode: Suitable for reward scenes, users can close the interactive ad interface after participating in the activity once, and closing the interface triggers the reward callback.
AppLuck.instance.openInteractiveAds(placementId, mode);

// Invoke the webview and load the activity, please pass in placementId
// mode 
//-- 0. Default mode: Suitable for fixed entrance scenes such as floating banner, users can freely close the interactive ad interface.
//-- 1. Interstitial mode: Suitable for interstitial scenes, users can close it after 10 seconds.
//-- 2. Reward mode: Suitable for reward scenes, users can close the interactive ad interface after participating in the activity {times} times, and closing the interface triggers the reward callback.
// times
//-- Used to limit the number of activity participations required by users when mode is 2 (reward mode).
AppLuck.instance.openInteractiveAds(placementId, mode, times);
```

- Set up the entrance manually, wait for Appluck to preload before displaying

```c#
// Hide the entrance material game object placement by default during game initialization
placement.gameObject.SetActive(false);

// Bind click event to placement
placement.onClick.AddListener(() =>
{
    // Invoke the webview and load the activity, please pass in placementId
    // mode 
    //-- 0. Default mode: Suitable for fixed entrance scenes such as floating banner, users can freely close the interactive ad interface.
    //-- 1. Interstitial mode: Suitable for interstitial scenes, users can close it after 10 seconds.
    //-- 2. Reward mode: Suitable for reward scenes, users can close the interactive ad interface after participating in the activity once, and closing the interface triggers the reward callback.
    AppLuck.instance.openInteractiveAds(placementId, mode);
});

// Display placement in the callback when SDK initialization is successful
AppLuckEvents.onInitSuccessEvent += () =>{
    placement.gameObject.SetActive(true);
}

### 4.3 Other Events
```c#
// AppLuck Close Callback
// status 0: Normal close; 1: Completed incentive task
AppLuckEvents.onInteractiveAdsHidden += (placementId, status) => {
    toast("onInteractiveAdsHidden: " + placementId + ", " + status);
};
```

[alup]: https://github.com/jxsong1989/Appluck_SDK_Unity/releases/tag/v1.2.3
