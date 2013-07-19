//
//  FacebookUnityPlugin.m
//  test
//
//  Created by wangusin on 13/6/11.
//  Copyright (c) 2013年 wes. All rights reserved.
//

#import "FacebookUnityPlugin.h"

@implementation FacebookUnityPlugin
@synthesize session;
@synthesize appId;
@synthesize permissions;
@synthesize unityFBEventDispatcher;


static FacebookUnityPlugin *singletonInstance = nil;

+ (FacebookUnityPlugin *) getSingleton {
    if (singletonInstance != nil) {
        
        return singletonInstance;
        
    } else {
        singletonInstance = [[FacebookUnityPlugin alloc] init];
        
        return singletonInstance;
    }
}

- (id)init {
    self = [super init];
    self.unityFBEventDispatcher = [[UnityFacebookEventDispatcher alloc] init];
    return self;
}

- (void) initFBSession {
    //NSArray* permissions = @[@"email", @"user_likes"];
    try {
        //    self.session = [[FBSession alloc] initWithAppID:@"108740425826087"
        //                                        permissions:permissions
        //                                    urlSchemeSuffix:nil
        //                                 tokenCacheStrategy:nil];
        //self.session = [[FBSession alloc] init];
        self.session = [[FBSession alloc] initWithPermissions:self.permissions];
    } catch (NSException *exception) {
        NSLog(@"exception : %@", [exception reason]);
    }

}


- (void)sessionStateChanged:(FBSession *)fbSession
                      state:(FBSessionState) state
                      error:(NSError *)error {
    NSString *accessTokenData;
    switch (fbSession.state) {
        case FBSessionStateOpen:
            //因為取得facebook user data 是透過 FBSession 去取得 所以需設值
            [FBSession setActiveSession:self.session];
            accessTokenData = fbSession.accessTokenData.accessToken;
            NSLog(@"after login token : %@",accessTokenData);
            [FacebookEventHandling fbLoginSuccess:accessTokenData];
            break;
            
        case FBSessionStateClosed:
        case FBSessionStateClosedLoginFailed:
            [FBSession.activeSession closeAndClearTokenInformation];
            NSLog(@"after logut token : %@",fbSession.accessTokenData.accessToken);
            [FacebookEventHandling fbLogoutSuccess];
            break;
            
        default:
            break;
    }
    
    if (error) {
        UIAlertView *alertView = [[UIAlertView alloc]
                                  initWithTitle:@"Error"
                                  message:error.localizedDescription
                                  delegate:nil
                                  cancelButtonTitle:@"OK"
                                  otherButtonTitles:nil];
        [alertView show];
    }
}


- (BOOL) fb_IsSessionVaild {
    
    if (self.session.isOpen) {
        
        if (self.session.state != FBSessionStateCreated) {
            
            return false;
        
        } else {
            
            return true;
        }
    
    } else {
        [self initFBSession];
        
        if (self.session .state == FBSessionStateCreatedTokenLoaded) {
            [self.session openWithCompletionHandler:^(FBSession *session,
                                                      FBSessionState status,
                                                      NSError *error) {
            [self sessionStateChanged:self.session state:status error:error];
            }];
            
            return true;
            
        } else {
            return false;
        }
    }
}

- (void) fb_Login {
    
    if (!self.session.isOpen || self.session.state != FBSessionStateCreated) {
        [self initFBSession];
    }
    
    try {
        [self.session openWithCompletionHandler:^(FBSession *session,
                                                  FBSessionState status,
                                                  NSError *error) {
            [self sessionStateChanged:self.session state:status error:error];
        }];
    } catch (NSException *exception) {
             NSLog(@"fb_Login exception : %@", [exception reason]);
    }
}

- (void) fb_Logout {
    if (self.session.isOpen) {
        [self.session closeAndClearTokenInformation];
    }
}

- (void) fb_RequestUserInfomation {
    if (FBSession.activeSession.isOpen) {

        [FBSettings setLoggingBehavior:[NSSet setWithObject:FBLoggingBehaviorFBRequests]];
        
        [[FBRequest requestForMe] startWithCompletionHandler:^(FBRequestConnection *connection,
                                                               NSDictionary<FBGraphUser> *user,
                                                               NSError *error) {
            if (!error) {
                 NSLog(@"user.name : %@", user.name);
                 NSLog(@"user.email : %@", [user objectForKey:@"email"]);
                 [FacebookEventHandling fbReQuestUserInfo:user email:[user objectForKey:@"email"]];
             }
            if (error) {
                UIAlertView *alertView = [[UIAlertView alloc]
                                          initWithTitle:@"Error"
                                          message:error.localizedDescription
                                          delegate:nil
                                          cancelButtonTitle:@"OK"
                                          otherButtonTitles:nil];
                [alertView show];
            }
         }];
    }
}


@end

extern "C" {
    void fbControllerInit(const char *appId, const char *permissions) {
        FacebookUnityPlugin *fbPlugin = [FacebookUnityPlugin getSingleton];
        fbPlugin.appId = [NSString stringWithUTF8String:appId];
        NSString *permissionsTmp = [NSString stringWithUTF8String:permissions];
        fbPlugin.permissions = [permissionsTmp componentsSeparatedByString:@","];
    }

    bool fbControllerIsSessionVaild() {
        NSLog(@"[facebookUnityPlugin]isSessionVaild");
        FacebookUnityPlugin *fbPlugin = [FacebookUnityPlugin getSingleton];
        return [fbPlugin fb_IsSessionVaild];
    }

    void fbControllerLogin() {
        FacebookUnityPlugin *fbPlugin = [FacebookUnityPlugin getSingleton];
        [fbPlugin fb_Login];
        NSLog(@"[facebookUnityPlugin] fb_Login...");
    }

    void fbControllerLogout() {
        FacebookUnityPlugin *fbPlugin = [FacebookUnityPlugin getSingleton];
        [fbPlugin fb_Logout];
        NSLog(@"[facebookUnityPlugin] fb_Logout");
    }
    
    void fbControllerRequestUserInfo() {
        FacebookUnityPlugin *fbPlugin = [FacebookUnityPlugin getSingleton];
        [fbPlugin fb_RequestUserInfomation];
        NSLog(@"[facebookUnityPlugin] fb_RequestUserInfomation");
    }
}
