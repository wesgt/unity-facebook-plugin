using System;
using UnityEngine;
using LitJson;
using com.softstar.unity.social;

public class meCallback :socalAPICallback
{
    public meCallback ()
    {
    }

    public override void onCompleted(string text){
        #if UNITY_EDITOR || UNITY_STANDALONE
        facebookAPI.User me = JsonMapper.ToObject<facebookAPI.User>(text);
        if (null != me)
        {
            Debug.Log("meCallback : " + me.name);
            Facebook.getInstance().onFacebookRequestInfo(me.name + "#FB#" + me.id + "#FB#" + me.email);
        }
        #endif

    }

    public override void onTimeout(){
        Debug.Log("Timeout");
    }

    public override void onError(string error){
        Debug.Log("Error:"+error);
    }
    public override void onAuthFail(){
        Debug.Log("AuthFail");
    }
}

