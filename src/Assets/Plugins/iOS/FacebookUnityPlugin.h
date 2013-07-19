//
//  FacebookUnityPlugin.h
//  test
//
//  Created by wangusin on 13/6/11.
//  Copyright (c) 2013年 wes. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "FacebookSDK.h"
#import "FacebookEventHandling.h"
#import "UnityFacebookEventDispatcher.h"

@interface FacebookUnityPlugin : NSObject {
    
}

@property (strong, nonatomic) FBSession *session;
@property (strong, nonatomic) NSString *appId;
@property (strong, nonatomic) NSArray *permissions;
@property (strong, nonatomic) UnityFacebookEventDispatcher *unityFBEventDispatcher;

- (BOOL) fb_IsSessionVaild;
- (void) fb_Login;
- (void) fb_Logout;
- (void) initFBSession;
- (void) fb_RequestUserInfomation;
+ (FacebookUnityPlugin *) getSingleton;


@end
