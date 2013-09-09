# UnitySocailPlugin

目前已完成 Facebook 的串接。
此 FB 的套件整合 IOS(facebook-ios-sdk3.5.3)、OSX、WINDOWS。

## Download

[unitysocial.unitypackage](http://rtd.softstar.com.tw/softstar-unity/unitysocial/blob/master/installation/unitysocial.unitypackage)

## Use Steps

### 1. Import unitysocial

開啟專案，載入 unitysocial.unitypackage

### 2. Facebook prefab setting

拖拉 "Facebook" Prefab 到您的 scene 中，點選 scene 中的 "Facebook" ，然後您可在 "Inspector" 看到五個參數。

* APP_ID - **YOUR_FACEBOOK_APP_ID**
* PERMISSIONS - **應用程式授權的範圍**
* EDITOR_TOKEN_FROM_FILE - **Editor 的版本中， facebook token 存放處**
* MINI_BROWSER_WIN_PATH - **Editor 的 win 版本中， minibrowser 放置處**
* MINI_BROWSER_OSX_PATH - **Editor 的 OSX 版本中， minibrow ser 放置處**

![image](http://rtd.softstar.com.tw/softstar-unity/unitysocial/raw/master/doc/images/facebook_setting.jpg)


### 3. 設定 fb_config.ini，將在plist 中自動生成FB 相關設定

modify Assets/Editor/fb_config.ini

    FB_ID - YOUR_FACEBOOK_APP_ID
    FB_DISPLAY_NAME - YOUR_FACEBOOK_DISPLAY_NAME


### 4. Social event handling

For example, if you want to 'listen' to a onlogin event:

```cs
public void initEventHandler() {
    SocialEvents.onLogin += onLogin;
}

public void onLogin(string accessToken) {
    Debug.Log("[SocialEventHandler] onLogin");
    Debug.Log("[SocialEventHandler] accessToken : " + accessToken);
}
```

### 5. Init SocialNetworkAPI ,SocialEventHandler

選擇您要使用的社群類型，目前只有 facebook ，並初始化。

```cs
SocialNetworkAPI socialNetworkAPI = SocialNetworkAPI.newSocialType(SocialNetworkAPI.FACEBOOK);
socialNetworkAPI.initialize();
```
初始化 SocialEventHandler。

```cs
SocialEventHandler socialEventHandler = new SocialEventHandler(this);
socialEventHandler.initEventHandler();
```

## Build IOS XCODE Project

修改 class 裡面 AppController.mm , 新增部分如下：

```ruby
#import "FBAppCall.h"
#import "FacebookUnityPlugin.h"

- (BOOL)application:(UIApplication *)application
            openURL:(NSURL *)url
  sourceApplication:(NSString *)sourceApplication
         annotation:(id)annotation
{
    return [FBAppCall handleOpenURL:url
        sourceApplication:sourceApplication
        withSession:[FacebookUnityPlugin getSingleton].session];
}
```
![image](http://rtd.softstar.com.tw/softstar-unity/unitysocial/raw/master/doc/images/facebook_xcode_bulid.jpg)

## Develop

如要新增新的社群平台，請繼承 SocialNetworkAPI

## Third party plugins

* [facebook-ios-sdk3.5.3](https://github.com/facebook/facebook-ios-sdk)
* [minibrowser]()
