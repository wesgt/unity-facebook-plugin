using System;
using UnityEngine;

namespace com.softstar.unity.social
{
    public abstract class SocialNetworkAPI
    {
        public const string FACEBOOK = "facebook";
        
        public abstract void initialize();
        public abstract bool isSessionVaild();
        public abstract void login();
        public abstract void logout();
        public abstract void requestUserInfomation();
        
        public static SocialNetworkAPI newSocialType(String socialType) {
        
            switch(socialType) {
            case FACEBOOK:
                return new FacebookAPI();
            default:
                Debug.LogError("ERROR_SOCIAL_TYPE");
                throw new ArgumentException("socialType error : " + socialType);
            }
        }
        
        public SocialNetworkAPI ()
        {
        }
    }
}

