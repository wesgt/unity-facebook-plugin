using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace com.softstar.unity.social
{
    public class FacebookAPI : SocialNetworkAPI
    {
        #if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
        /// <summary>
        /// Facebook init
        /// </summary>
        public static extern void fbControllerInit(string appId, string permissions);
        [DllImport("__Internal")]
        /// <summary>
        /// Facebook login
        /// </summary>
        public static extern void fbControllerLogin();

        [DllImport("__Internal")]
        /// <summary>
        /// Returns true if the current session is valid
        /// </summary>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// </returns>
        public static extern bool fbControllerIsSessionVaild();

        [DllImport("__Internal")]
        /// <summary>
        /// Facebook logout
        /// </summary>
        public static extern void fbControllerLogout();
        
        [DllImport("__Internal")]
        /// <summary>
        /// Facebook requestUserInfomation
        /// </summary>
        public static extern void fbControllerRequestUserInfo();
        #endif
        public override void initialize() 
        {
            Debug.Log("[FacebookAPI] init");
            Debug.Log("[FacebookAPI] APP_ID : " + Facebook.getInstance().APP_ID);
            Debug.Log("[FacebookAPI] PERMNISSIONS : " + Facebook.getInstance().PERMISSIONS);
            #if UNITY_IPHONE && !UNITY_EDITOR
            Debug.Log("Unity ios");
            fbControllerInit(Facebook.getInstance().APP_ID, Facebook.getInstance().PERMISSIONS);
            #endif
            #if UNITY_EDITOR || UNITY_STANDALONE
            Debug.Log("Unity Editor");
            Facebook.getInstance().initFBWindowsAPI();
            #endif
        }

        public override bool isSessionVaild()
        {
            #if UNITY_IPHONE && !UNITY_EDITOR
            return fbControllerIsSessionVaild();
            #endif
            #if UNITY_EDITOR || UNITY_STANDALONE
            facebookAPI fbWindowsAPI = Facebook.getInstance().getFBWindowsAPI();
            return fbWindowsAPI.hasAccessToken(); 
            #endif
        }

        public override void login()
        {
            #if UNITY_IPHONE && !UNITY_EDITOR
            fbControllerLogin();
            #endif
            #if UNITY_EDITOR || UNITY_STANDALONE
            facebookAPI fbWindowsAPI = Facebook.getInstance().getFBWindowsAPI();

            bool canPost = fbWindowsAPI.loginByminiBrowser(Facebook.getInstance().EDITOR_TOKEN_FROM_FILE);

            if (canPost)
            {
                fbWindowsAPI.loadSettingFromFile(Facebook.getInstance().EDITOR_TOKEN_FROM_FILE);
                Facebook.getInstance().onFacebookLogin("");
            }

            #endif
        }

        public override void logout()
        {
            #if UNITY_IPHONE && !UNITY_EDITOR
            fbControllerLogout();
            #endif
            #if UNITY_EDITOR || UNITY_STANDALONE
            Facebook.getInstance().onFacebookLogout("");
            #endif
        }

        public override void requestUserInfomation()
        {
            #if UNITY_IPHONE && !UNITY_EDITOR
            fbControllerRequestUserInfo();
            #endif
            #if UNITY_EDITOR || UNITY_STANDALONE
            facebookAPI fbWindowsAPI = Facebook.getInstance().getFBWindowsAPI();
            fbWindowsAPI.getMe(20, new meCallback());
            #endif
        }
    }
}

