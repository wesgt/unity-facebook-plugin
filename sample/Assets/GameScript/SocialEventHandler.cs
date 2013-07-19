using System;
using UnityEngine;
using com.softstar.unity.social;
using System.Text;

namespace com.softstar.unity.game
{
    public class SocialEventHandler
    {
        FBApplication fbApplication;
        public SocialEventHandler (FBApplication fbApplication)
        {
            this.fbApplication = fbApplication;
        }

        public void initEventHandler() {
            SocialEvents.onLogin += onLogin;
            SocialEvents.onLogout += onLogout;
            SocialEvents.onRequestInfo += onRequestInfo;
        }
        
        public void onLogin(string accessToken) {
            Debug.Log("[SocialEventHandler] onLogin");
            Debug.Log("[SocialEventHandler] accessToken : " + accessToken);
            try {

                fbApplication.loginState = FBApplication.LoginState.login;

            } catch (Exception ex) {
                Debug.Log("exception : " + ex);
            }

        }

        public void onLogout() {
            Debug.Log("[SocialEventHandler] onLogout");
            try {

                fbApplication.loginState = FBApplication.LoginState.unlogin;
                fbApplication.userInfo = "";

            } catch (Exception ex) {
                Debug.Log("exception : " + ex);
            }

        }

        public void onRequestInfo(SocialUser socialUser) {
            Debug.Log("[SocialEventHandler] onRequestInfo");
            StringBuilder userInfoTmp = new StringBuilder();
            userInfoTmp.Append("fbId:").Append(socialUser.fbId)
                .Append(" name : ").Append(socialUser.name)
                .Append(" email : ").Append(socialUser.email);
            Debug.Log("[SocialEventHandler] serInfo" + userInfoTmp.ToString());
            fbApplication.userInfo = userInfoTmp.ToString();
        }
    }
}

