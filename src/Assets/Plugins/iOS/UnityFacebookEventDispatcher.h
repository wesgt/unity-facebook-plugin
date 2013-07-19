//
//  UnityFacebookEventDispatcher.h
//  Unity-iPhone
//
//  Created by wangusin on 13/6/17.
//
//

#import <Foundation/Foundation.h>

@interface UnityFacebookEventDispatcher : NSObject {
    
}

- (id)init;
- (void)handleEvent:(NSNotification *)notification;
@end
