//
//  FacebookEventHandling.m
//  Unity-iPhone
//
//  Created by wangusin on 13/6/17.
//
//

#import "FacebookEventHandling.h"

@implementation FacebookEventHandling

+ (void) observeAllEventsWithObserver:(id)observer withSelector:(SEL)selector {
    [[NSNotificationCenter defaultCenter] addObserver:observer selector:selector name:EVENT_FB_LOGIN_SUCCESS object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:observer selector:selector name:EVENT_FB_LOGOUT_SUCCESS object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:observer selector:selector name:EVENt_FB_REQUEST_USER_INFO object:nil];
    
}

+ (void) fbLoginSuccess:(NSString *) token {
    NSMutableDictionary *userInfo = [NSMutableDictionary dictionary];
    [userInfo setValue:token forKey:FB_ACCESS_TOKEN];
    
    [[NSNotificationCenter defaultCenter] postNotificationName:EVENT_FB_LOGIN_SUCCESS object:self userInfo:userInfo];
}

+ (void) fbLogoutSuccess {
    [[NSNotificationCenter defaultCenter] postNotificationName:EVENT_FB_LOGOUT_SUCCESS object:self];
}

+ (void)fbReQuestUserInfo:(NSDictionary<FBGraphUser> *)user email:(NSString *)email{
    NSMutableDictionary *userInfo = [NSMutableDictionary dictionary];
    [userInfo setObject:user forKey:FB_USER];
    [userInfo setValue:email forKey:FB_EMAIL];
    
    [[NSNotificationCenter defaultCenter] postNotificationName:EVENt_FB_REQUEST_USER_INFO object:self userInfo:userInfo];
}
@end
