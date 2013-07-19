using UnityEngine;
using System.Collections;
using com.softstar.unity.social;
using System;


namespace com.softstar.unity.game
{
    public class FBApplication : MonoBehaviour {

        public LoginState loginState {get;set;}
        public string userInfo {get;set;}
        public enum LoginState {login, unlogin};
        SocialNetworkAPI socialNetworkAPI;


        // Use this for initialization
        void Start () {
            Debug.Log("[FBAppLiscation] init");
            socialNetworkAPI = SocialNetworkAPI.newSocialType(SocialNetworkAPI.FACEBOOK);
            socialNetworkAPI.initialize();
            SocialEventHandler socialEventHandler = new SocialEventHandler(this);
            socialEventHandler.initEventHandler();
            
            if (!socialNetworkAPI.isSessionVaild()) {
                loginState = LoginState.unlogin;
                
            } else {
                loginState = LoginState.login;
            }

            Debug.Log("[FBAppLiscation] end");
        }

        void OnGUI() {
            GUI.Label(new Rect(10, 10, 600, 30), "UnitySocial Test");

            switch (loginState) {
            case LoginState.login:
                drawLoginPage();
                break;
            case LoginState.unlogin:
                drawUnLoginPage();
                break;
            default:
                break;
            }

        }

        void drawUnLoginPage() {
            if (GUI.Button(new Rect(10, 70, 100, 30), "login")) {
                Debug.Log("Clicked login");
                socialNetworkAPI.login();
            }
        }

        void drawLoginPage() {
            if (GUI.Button(new Rect(10, 110, 100, 30), "logout")) {
                Debug.Log("Clicked logout");
                socialNetworkAPI.logout();
            }

            if (GUI.Button(new Rect(10, 150, 100, 30), "getUserInfo")) {
                Debug.Log("Clicked getUserInfo");
                socialNetworkAPI.requestUserInfomation();
            }
            GUI.Label(new Rect(10, 190, 600, 30), userInfo);
        }
    }

}

