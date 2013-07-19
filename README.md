UnitySocailPlugin
============
* * *

目前已完成Facebook的串接
此FB的套件整合IOS(facebook-ios-sdk3.5.3)、OSX、WINDOWS，如要使用請參照Installation

Installation & Setting
------------

* import installation/unitysocial.unitypackage
* modify Assets/Editor/fb_config.ini

>FB_ID= **YOUR_FACEBOOK_APP_ID**
>FB_DISPLAY_NAME = **YOUR_FACEBOOK_DISPLAY_NAME**

Social Event Handling
------------

For example, if you want to 'listen' to a onlogin event:

```ruby
public void initEventHandler() {
    SocialEvents.onLogin += onLogin;
}

public void onLogin(string accessToken) {
    Debug.Log("[SocialEventHandler] onLogin");
    Debug.Log("[SocialEventHandler] accessToken : " + accessToken);
}
```


Build IOS XCODE Setting
------------

修改class裡面AppController.mm
在最上面import部分增加下兩行

```ruby
#import "FBAppCall.h"
#import "FacebookUnityPlugin.h"

//在最後面貼上
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

