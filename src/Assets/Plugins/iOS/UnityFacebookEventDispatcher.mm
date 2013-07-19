//
//  UnityFacebookEventDispatcher.m
//  Unity-iPhone
//
//  Created by wangusin on 13/6/17.
//
//

#import "UnityFacebookEventDispatcher.h"
#import "FacebookEventHandling.h"

@implementation UnityFacebookEventDispatcher

- (id)init {
 
    if (self = [super init]) {
        [FacebookEventHandling observeAllEventsWithObserver:self withSelector:@selector(handleEvent:)];
    }
    return self;
}

- (void)handleEvent:(NSNotification *)notification {
    
    NSLog(@"notification.name : %@",notification.name);
    
    if ([notification.name isEqualToString:EVENT_FB_LOGIN_SUCCESS]) {
        NSDictionary *userInfo = [notification userInfo];
        NSString *accessToken = [userInfo objectForKey:FB_ACCESS_TOKEN];
        UnitySendMessage("Facebook", "onFacebookLogin", [[NSString stringWithFormat:@"%@", accessToken] UTF8String]);
        
    } else if ([notification.name isEqualToString:EVENT_FB_LOGOUT_SUCCESS]) {
        UnitySendMessage("Facebook", "onFacebookLogout", "");
        
    } else if ([notification.name isEqualToString:EVENt_FB_REQUEST_USER_INFO]) {
        NSDictionary *userInfo = (NSDictionary*)[notification userInfo];
        NSDictionary<FBGraphUser> *fbUser = (NSDictionary<FBGraphUser> *)[userInfo objectForKey:FB_USER];
        NSString *fbEmail = (NSString *)[userInfo objectForKey:FB_EMAIL];
        NSString *userName = fbUser.name;
        NSString *userProfileId = fbUser.id;
        //NSString *userEmail = userInfo.email;
        UnitySendMessage("Facebook", "onFacebookRequestInfo", [[NSString stringWithFormat:@"%@#FB#%@#FB#%@", userName, userProfileId, fbEmail] UTF8String]);
    }
}

@end
