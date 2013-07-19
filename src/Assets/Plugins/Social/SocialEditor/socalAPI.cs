using System;
using System.IO;
using System.Diagnostics;
using UnityEngine;

public abstract class socalAPI
{
    public enum mesageScope :int {
        mesageScope_public         = 0,
        mesageScope_self         = 1,
        mesageScope_myFriend     = 2,
    };
    public socalAPI (string appKey, string appSecret, string myClassName, string redirectURL, string scope)
    {
        m_appKey = String.Copy(appKey);
        m_appSecret = String.Copy(appSecret);
        m_className = String.Copy(myClassName);
        socalAPIDispatcher.Init();
        m_redirectURL = String.Copy(redirectURL);
        if (null != scope)
            m_scope = String.Copy(scope);
    }

    public abstract bool postMessage( mesageScope scope, string message, int timeout, socalAPICallback callback);

    public abstract bool postMessageWithPhoto( mesageScope scope, string message, string phtotFilePath, int timeout, socalAPICallback callback);

    public abstract socalAPIRequest.errorMessage parseError(string text);

    public abstract bool isAuthFail(int error);

    public string className{
        get{
            return m_className;
        }
    }

    public bool hasAccessToken() {
        if (null != m_accesstoken){
            return (0 < m_accesstoken.Length);
        }
        else{
            return false;
        }
    }

    public bool loadSettingFromFile( string filepath ){

        FileInfo fileInfo = new FileInfo(filepath);

        if (fileInfo.Exists){
            //int filelength = (int)fileInfo.Length;
            StreamReader fileReader = System.IO.File.OpenText(filepath );
            string encodeString = fileReader.ReadToEnd();
            byte[] decodeString = System.Convert.FromBase64String(encodeString);
            string realString = System.Text.Encoding.UTF8.GetString(decodeString);
            bool result = serialize(realString, false);
            fileReader.Close();
            m_settingfilepath = String.Copy(filepath);
            return result;
        }
        return false;
    }

    protected virtual bool serialize(string data , bool store )    {
        bool result = true;
        if (store){
            data += "access_token,"+m_accesstoken+",";
            data += "expires_in,"+m_expires_in+",";
        }
        else{
            string appKey = null;
            string [] separator = {","};
            string[] tokens = data.Split( separator , StringSplitOptions.RemoveEmptyEntries);

            for(int i=0;i<tokens.Length;i++){
                string key = (string)tokens.GetValue(i);
                string value = (string)tokens.GetValue(++i);
                if (key == "access_token"){
                    m_accesstoken = String.Copy(value);
                }
                else if (key == "expires_in"){
                    m_expires_in = String.Copy(value);
                }
                else if (key == "appkey"){
                    appKey = value;
                }
            }

            if (m_appKey == appKey)
                m_appKey = appKey;
            else{
                m_accesstoken = null;
                return false;
            }

        }
        return result;
    }

    public bool loginByminiBrowser(string filepath){
        Int32 lastIndex = filepath.LastIndexOf("/");
        string fileFolder = filepath.Substring(0, lastIndex + 1);
        string fileName = filepath.Substring(lastIndex + 1);

        UnityEngine.Debug.Log("miniBrowserPath : " + miniBrowserPath);
        Process  minibrowserLuncher = new Process();
        minibrowserLuncher.StartInfo.UseShellExecute = false;
        minibrowserLuncher.StartInfo.FileName = miniBrowserPath;
        minibrowserLuncher.StartInfo.Arguments = "-socaltype"+" "+ className+" ";
        minibrowserLuncher.StartInfo.Arguments += "-appkey"+" "+ m_appKey+" ";
        minibrowserLuncher.StartInfo.Arguments += "-appsecret"+" "+ m_appSecret+" ";
        minibrowserLuncher.StartInfo.Arguments += "-redirecturl"+" "+ m_redirectURL+" ";
        minibrowserLuncher.StartInfo.Arguments += "-scope"+" "+ m_scope +" ";
        minibrowserLuncher.StartInfo.Arguments += "-outputfolder"+" "+ fileFolder+" ";
        minibrowserLuncher.StartInfo.Arguments += "-outputfilename"+" "+ fileName+" ";

        UnityEngine.Debug.Log("miniBrowserPath..end");
        if (minibrowserLuncher.Start())
        {
            minibrowserLuncher.WaitForExit();
            socalAPIDispatcher.Instance().RefreshLastRunTime();
            bool result = (1 == minibrowserLuncher.ExitCode);
            return result;
        }
        return false;
    }

    public byte[] loadFile(string phtotFilePath)
    {
        // Load file
        FileInfo fileinfo = new FileInfo(phtotFilePath);
        int filelength = (int)fileinfo.Length;
        BinaryReader fileReader = new BinaryReader(System.IO.File.Open (phtotFilePath, FileMode.Open,FileAccess.Read));
        byte[] fileContent = fileReader.ReadBytes(filelength);
        fileReader.Close();
        return fileContent;
    }

    public void releaseAccessToken(){
        if (null != m_accesstoken){
            m_accesstoken = null;
            System.IO.File.Delete(m_settingfilepath);
        }
    }

    public string miniBrowserPath {
        get {
            return m_miniBrowserPath;
        }
        set {
            m_miniBrowserPath = value;
        }
    }
    private string m_className;
    protected string m_accesstoken;
    protected string m_expires_in;
    protected string m_appKey;
    protected string m_appSecret;
    protected string m_settingfilepath;
    protected string m_miniBrowserPath;
    protected string m_redirectURL;
    protected string m_scope;
}

