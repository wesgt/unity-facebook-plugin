UnitySocailPlugin
============
* * *

目前已完成Facebook的串接。
此FB的套件整合IOS(facebook-ios-sdk3.5.3)、OSX、WINDOWS，如要使用請參照Getting Start。

Getting Started
------------

* import installation/unitysocial.unitypackage
* 拖拉"Facebook" Prefab 到您的scene中，點選scene中的"Facebook"，然後您可在"Inspector"看到五個參數。
![image](http://172.18.106.90/unity/unitysocial/doc/images/raw/master/facebook_setting.jpg)
    - APP_ID - **YOUR_FACEBOOK_APP_ID**
    - PERMISSIONS - **應用程式授權的範圍**
    - EDITOR_TOKEN_FROM_FILE - **Editor的版本中，facebook token存放處**
    - MINI_BROWSER_WIN_PATH - **Editor的win版本中，minibrowser放置處**
    - MINI_BROWSER_OSX_PATH - **Editor的OSX版本中，minibrowser放置處**
* modify Assets/Editor/fb_config.ini
    - FB_ID - **YOUR_FACEBOOK_APP_ID**
    - FB_DISPLAY_NAME - **YOUR_FACEBOOK_DISPLAY_NAME**
* Social Event Handling

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

* 初始化SocialNetworkAPI、SocialEventHandler(參照Socail Event Handling範例)
    - 選擇您要使用的社群類型，目前只有facebook，並初始化。

    ```cs
    SocialNetworkAPI socialNetworkAPI = SocialNetworkAPI.newSocialType(SocialNetworkAPI.FACEBOOK);
    socialNetworkAPI.initialize();
    ```
    - 初始化SocialEventHandler。

    ```cs
    SocialEventHandler socialEventHandler = new SocialEventHandler(this);
    socialEventHandler.initEventHandler();
    ```

Build IOS XCODE Setting
------------

* 修改class裡面AppController.mm

    ![image](http://172.18.106.90/unity/unitysocial/doc/images/raw/master/facebook_xcode_bulid.jpg)

    - 在最上面import部分增加下兩行

    ```ruby
    #import "FBAppCall.h"
    #import "FacebookUnityPlugin.h"
    ```

    - 在最後面的end前面貼上

    ```ruby
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
* Run

Develop
------------

* 如要新增新的社群平台，請繼承SocialNetworkAPI

Third party plugins
------------

* [facebook-ios-sdk3.5.3](https://github.com/facebook/facebook-ios-sdk)
* [minibrowser]()
