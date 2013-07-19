using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

namespace com.softstar.unity.social
{
    public class Facebook : MonoBehaviour
    {
        public string APP_ID = "108740425826087";
        public string PERMISSIONS = "email,user_likes,basic_info";
        public string EDITOR_TOKEN_FROM_FILE = "minibrowser/config/facebook.txt";
        public string MINI_BROWSER_WIN_PATH = "minibrowser/minibrowserWin/minibrowser.exe";
        public string MINI_BROWSER_OSX_PATH = "minibrowser/minibrowserOSX/minibrowser.app/Contents/MacOS/minibrowser";
        private static Facebook instance = null;
        private facebookAPI fbWindowsAPI;


        void Awake() {
            Debug.Log("[Facebook] Awake");
            Debug.Log("Application.dataPath : " + Application.dataPath);
            
            if (APP_ID.Length == 0) {
                Debug.LogError("APP_ID NO VALUE");
            }

            if (instance == null) {
                instance = this;
                GameObject.DontDestroyOnLoad(this.gameObject);
            }else {
                GameObject.Destroy(this.gameObject);
            }
        }


        public static Facebook getInstance() {
            return instance;
        }

        public void onFacebookLogin(string message) {
            Debug.Log("[Facebook] onFacebookLogin");
            string accessToken = message;
            Debug.Log("[Facebook] accessToken : " + accessToken);
            SocialEvents.onLogin(accessToken);
        }

        public void onFacebookLogout(string message) {
            Debug.Log("[Facebook] onFacebookLogout");
            SocialEvents.onLogout();
        }

        public void onFacebookRequestInfo(string message) {
            Debug.Log("[Facebook] onFacebookRequestInfo");
            string[] vars = Regex.Split(message, "#FB#");
            SocialUser socialUser = new SocialUser();
            socialUser.name = vars[0];
            socialUser.fbId = vars[1];
            socialUser.email = vars[2];
            SocialEvents.onRequestInfo(socialUser);
        }

        public void initFBWindowsAPI()
        {
            fbWindowsAPI = new facebookAPI(APP_ID, "cdfceab8609b8b55c449a507f54bd635",
                "https://www.facebook.com/connect/login_success.html", PERMISSIONS);

            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                string dataPath = Application.dataPath;
                int assetsIndex = dataPath.IndexOf("Assets");
                string projectPath = dataPath.Substring(0, assetsIndex);
                fbWindowsAPI.miniBrowserPath = projectPath + MINI_BROWSER_OSX_PATH;

            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                fbWindowsAPI.miniBrowserPath = MINI_BROWSER_WIN_PATH;
            }
            else
            {
                fbWindowsAPI.miniBrowserPath = MINI_BROWSER_WIN_PATH;
            }
            Debug.Log("Application.platform : " + Application.platform);
            Debug.Log("miniBrowserPath : " + fbWindowsAPI.miniBrowserPath);

            fbWindowsAPI.loadSettingFromFile(EDITOR_TOKEN_FROM_FILE);
        }

        public facebookAPI getFBWindowsAPI()
        {
            return fbWindowsAPI;
        }
    }
}

