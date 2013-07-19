//
//  FacebookEventHandling.h
//  Unity-iPhone
//
//  Created by wangusin on 13/6/17.
//
//

#import <Foundation/Foundation.h>
#import "FacebookSDK.h"
// Events
#define EVENT_FB_LOGIN_SUCCESS          @"fbLoginSuccess"
#define EVENT_FB_LOGOUT_SUCCESS         @"fbLogoutSuccess"
#define EVENt_FB_REQUEST_USER_INFO      @"fbRequestUserInfo"

// UserInfo Elements
#define FB_ACCESS_TOKEN                 @"fbAccessToken"
#define FB_USER                         @"fbUser"
#define FB_EMAIL                        @"fbEmail"

@interface FacebookEventHandling : NSObject

+ (void) observeAllEventsWithObserver:(id)observer withSelector:(SEL)selector;
+ (void) fbLoginSuccess:(NSString *) token;
+ (void) fbLogoutSuccess;
+ (void) fbReQuestUserInfo:(NSDictionary<FBGraphUser> *)user email:(NSString *)email;

@end
