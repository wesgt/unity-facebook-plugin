using UnityEngine;
using System.Collections;
using System.Diagnostics;


public class createprocess : MonoBehaviour {

    public class respone : socalAPICallback
    {
    public  override void onCompleted(string text){
            UnityEngine.Debug.Log("onCompleted:" + text);
        }
    public  override void onTimeout(){
            UnityEngine.Debug.Log("Timeout");
        }
    public  override void onError(string error){
            UnityEngine.Debug.Log("onError:"+error);

        }

    public  override void onAuthFail(){
            UnityEngine.Debug.Log("onAuthFail");

        }
    }

    // Use this for initialization
    void Start () {

        // test sina weibo
        //StartCoroutine(sina_test(2));

        // test qq weibo
        //StartCoroutine(qq_test(2));

        // test fackbook
        StartCoroutine(fb_test(2));

    }

    IEnumerator fb_test(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        facebookAPI api = new facebookAPI("167182563454056","cdfceab8609b8b55c449a507f54bd635","https://www.facebook.com/connect/login_success.html","publish_actions,publish_stream,user_photos");
        // 仙劍
        //facebookAPI api = new facebookAPI("163551776923","b3f66db035ff952f9ff3d1d6487b52d7","https://www.facebook.com/connect/login_success.html");

        api.miniBrowserPath = "./minibrowser.exe";
        api.loadSettingFromFile("config/facebook.txt");

        bool canPost = api.hasAccessToken();
        if (false == canPost){
            bool isFullScreen = Screen.fullScreen;
            if ( isFullScreen)
            {
                setResolution(Screen.width, Screen.height, false);
                yield return new WaitForSeconds(1);
            }

            canPost = api.loginByminiBrowser("config/1.txt");

            if (canPost)
                api.loadSettingFromFile("config/1.txt");

            if (isFullScreen){
                setResolution(Screen.width, Screen.height, true);
                yield return new WaitForSeconds(5);
            }
        }

        if (canPost){
//            api.postMessage(
//                socalAPI.mesageScope.mesageScope_self,
//                "test",
//                10,
//                new respone());

//            api.postMessageWithPhoto(
//                socalAPI.mesageScope.mesageScope_self,
//                "Hello world7",
//                "Hydrangeas.jpg",
//                50,
//                new respone());


            api.getMe(10, new meCallback());
        }
    }

    IEnumerator qq_test(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        qqWeiboAPI api = new qqWeiboAPI("801375266","980879af895c6323761a463112b25f12","http://swd.softstar.com.tw/index.aspx","");
        StartCoroutine(api.getRealIP());
        api.loadSettingFromFile("config/qq_weibo.txt");

        bool canPost = api.hasAccessToken();
        if (false == canPost){
            bool isFullScreen = Screen.fullScreen;
            if ( isFullScreen)
            {
                setResolution(Screen.width, Screen.height, false);
                yield return new WaitForSeconds(1);
            }

            canPost = api.loginByminiBrowser("config");

            if (canPost)
                api.loadSettingFromFile("config/qq_weibo.txt");

            if (isFullScreen){
                setResolution(Screen.width, Screen.height, true);
                yield return new WaitForSeconds(5);
            }
        }

        if (canPost){
//            api.postMessage(
//                socalAPI.mesageScope.mesageScope_self,
//                "test",
//                10,
//                new respone());

            api.postMessageWithPhoto(
                socalAPI.mesageScope.mesageScope_self,
                "Hello world7",
                "Hydrangeas.jpg",
                50,
                new respone());

        }
    }
    IEnumerator sina_test(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        // test sina weibo
        sinaWeiboAPI api = new sinaWeiboAPI("1014119558","707858d2c5e75940404a484157547620","https://api.weibo.com/oauth2/default.html","");
        api.loadSettingFromFile("config/sina_weibo.txt");
        bool canPost = api.hasAccessToken();
        if (false == canPost){
            bool isFullScreen = Screen.fullScreen;
            if ( isFullScreen)
            {
                setResolution(Screen.width, Screen.height, false);
                yield return new WaitForSeconds(1);
            }

            canPost = api.loginByminiBrowser("config");

            if (canPost)
                api.loadSettingFromFile("config/sina_weibo.txt");
            if (isFullScreen){
                setResolution(Screen.width, Screen.height, true);
                yield return new WaitForSeconds(5);
            }
        }
        if (canPost){
            api.postMessage(socalAPI.mesageScope.mesageScope_myFriend,
                "Hello ,how are you?",
                1,
                new respone());
            yield return new WaitForSeconds(5);
            api.postMessageWithPhoto(socalAPI.mesageScope.mesageScope_myFriend,
                "Desert",
                "Desert.jpg",
                5,
                new respone());

            yield return new WaitForSeconds(5);
            api.postMessageWithPhoto(socalAPI.mesageScope.mesageScope_myFriend,
                "Chrysanthemum",
                "Chrysanthemum.jpg",
                5,
                new respone());

            yield return new WaitForSeconds(5);
            api.postMessageWithPhoto(socalAPI.mesageScope.mesageScope_myFriend,
                "Hydrangeas",
                "Hydrangeas.jpg",
                5,
                new respone());

        }
        else{
            UnityEngine.Debug.Log("can not post ");
        }
    }

    void setResolution(int width, int height, bool fullScreen)
    {
        Screen.SetResolution(width, height, fullScreen);

    }
    // Update is called once per frame
    void Update () {

    }
}
