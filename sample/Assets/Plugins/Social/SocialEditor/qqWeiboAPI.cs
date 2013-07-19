using System;
using UnityEngine;
using System.Collections;
using LitJson;

public class qqWeiboAPI : socalAPI
{
    public qqWeiboAPI (string appKey,string appSecret, string redirectURL,string scope)
    : base(appKey,appSecret ,"qq_weibo", redirectURL, scope)
    {
        m_realIP = "114.134.85.39";        // t.qq.com, fake ip address
    }

    public override bool postMessage(mesageScope scope, string message, int timeout, socalAPICallback callback ){
        string url = "https://open.t.qq.com/api/t/add";

        WWWForm form = new WWWForm();
        form.AddField("oauth_consumer_key",m_appKey);
        form.AddField("openid",m_openId);
        form.AddField("access_token",m_accesstoken);
        form.AddField("oauth_version","2.a");
        form.AddField("clientip",m_realIP);
        //form.AddField("scope","all");

        form.AddField("format","json");
        form.AddField("content",message);

        socalAPIRequest request = new socalAPIRequest(url, form, callback, timeout ,this);
        socalAPIDispatcher.Instance().addCallback( request );

        return true;
    }

    public override bool postMessageWithPhoto( mesageScope scope, string message, string phtotFilePath, int timeout, socalAPICallback callback ){
        string url = "https://open.t.qq.com/api/t/add_pic";
        UnityEngine.WWWForm form = new UnityEngine.WWWForm();
        form.AddField("oauth_consumer_key",m_appKey);
        form.AddField("openid",m_openId);
        form.AddField("access_token",m_accesstoken);
        form.AddField("oauth_version","2.a");
        form.AddField("clientip",m_realIP);
        form.AddField("format","json");
        byte[] fileData = loadFile(phtotFilePath);
        if ((fileData == null )||(fileData.Length ==0 ))
            return false;
        form.AddField("content",message);
        form.AddBinaryData("pic", fileData,"abc.jpg","image/jpeg");
        byte[] formData = form.data;
        socalAPIRequest request = new socalAPIRequest(url, formData , form.headers , callback, timeout ,this);
        socalAPIDispatcher.Instance().addCallback( request );

        return true;
    }

    public override socalAPIRequest.errorMessage parseError(string text){
        try {
             ReturnValue Container = JsonMapper.ToObject<ReturnValue>(text);

            if (0 != Container.errcode )
            {
                socalAPIRequest.errorMessage error = new socalAPIRequest.errorMessage();
                error.Message = String.Copy(Container.msg);
                error.Code = Container.errcode;
                return error;
            }
            else
                return null;
        }
        catch {
            return null;
        }
    }

    public override bool isAuthFail(int error){
        switch (error){
        case 34:
        case 36:
        case 37:
        case 38:
            return true;
        default:
            return false;
        }
    }


    public class detailErrInfo{
        public string accesstoken{
                    get{
                        return m_accesstoken;
                    }
                    set{
                        m_accesstoken = value;
                    }
                }
        public string apiname{
                    get{
                        return m_apiname;
                    }
                    set{
                        m_apiname = value;
                    }
                }
        public string appkey{
                    get{
                        return m_appkey;
                    }
                    set{
                        m_appkey = value;
                    }
                }
        public string clientip{
                    get{
                        return m_clientip;
                    }
                    set{
                        m_clientip = value;
                    }
                }
        public int cmd{
                    get{
                        return m_cmd;
                    }
                    set{
                        m_cmd = value;
                    }
                }
        public int proctime{
                    get{
                        return m_proctime;
                    }
                    set{
                        m_proctime = value;
                    }
                }
        public int ret1{
                    get{
                        return m_ret1;
                    }
                    set{
                        m_ret1 = value;
                    }
                }
        public int ret2{
                    get{
                        return m_ret2;
                    }
                    set{
                        m_ret2 = value;
                    }
                }
        public int ret3{
                    get{
                        return m_ret3;
                    }
                    set{
                        m_ret3 = value;
                    }
                }
        public long ret4{
                    get{
                        return m_ret4;
                    }
                    set{
                        m_ret4 = value;
                    }
                }
        public long timestamp{
                    get{
                        return m_timestamp;
                    }
                    set{
                        m_timestamp = value;
                    }
                }

        string m_accesstoken;
        string m_apiname;
        string m_appkey;
        string m_clientip;
        int m_cmd;
        int m_proctime;
        int m_ret1;
        int m_ret2;
        int m_ret3;
        long m_ret4;
        long m_timestamp;
    }

    public class ReturnValue {
        public detailErrInfo detailerrinfo{
            get {
                return m_detailerrinfo;
            }
            set {
                m_detailerrinfo = value;
            }
        }

        public string data{
            get {
                return m_data;
            }
            set{
                m_data = value;
            }
        }

        public int errcode{
            get {
                return m_errcode;
            }
            set {
                m_errcode = value;
            }
        }

        public string msg {
            get {
                return m_msg;
            }
            set {
                m_msg = value;
            }
        }

        public int ret {
            get {
                return m_ret;
            }
            set {
                m_ret = value;
            }
        }

        detailErrInfo m_detailerrinfo;
        string m_data;
        int m_errcode;
        string m_msg;
        int m_ret;
    }


    public IEnumerator getRealIP(){
        WWW www = new WWW("http://www.instrument.com.cn/ip.aspx");
        yield return www;
        if (www.isDone){
            if (false == String.IsNullOrEmpty(www.error)){
                m_realIP = string.Copy(www.text);
            }
        }
    }

    protected override bool serialize(string data , bool store )    {
        bool resule = base.serialize(data , store);
        if (resule){
            if (store){
                data += "refresh_token,"+m_refresh_token+",";
                data += "openkey,"+m_openkey+",";
                data += "openid,"+m_openId+",";
            }
            else{
                string[] separator = {","};
                string[] tokens = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                for(int i=0;i<tokens.Length;i++){
                    string key = (string)tokens.GetValue(i);
                    string value = (string)tokens.GetValue(++i);
                    if (key == "openid"){
                        m_openId = value;
                    }
                    else if (key == "openkey"){
                        m_openkey = value;
                    }
                    else if (key == "refresh_token"){
                        m_refresh_token = value;
                    }
                }
            }
        }
        return resule;
    }

    private string m_openId;
    private string m_openkey;
    private string m_refresh_token;
    private string m_realIP;

}

