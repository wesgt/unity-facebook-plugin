using System;
using UnityEngine;
using LitJson;

public class sinaWeiboAPI :socalAPI
{
    public sinaWeiboAPI (string appKey, string appSecret, string redirectURL,string scope)
    : base(appKey,appSecret,"sina_weibo",redirectURL,scope)
    {

    }

    public override bool postMessage(mesageScope scope, string message, int timeout, socalAPICallback callback){
        string url = "https://api.weibo.com/2/statuses/update.json";
        WWWForm form = new WWWForm();
        form.AddField("access_token",m_accesstoken);
        form.AddField("status",message);
        int visible = 1;

        if (scope == socalAPI.mesageScope.mesageScope_public){
                visible = 0;
        }
        else if (scope == socalAPI.mesageScope.mesageScope_self){
                visible = 1;
        }
        else if (scope == socalAPI.mesageScope.mesageScope_myFriend){
                visible = 2;
        }


        form.AddField("visible",visible);
        socalAPIRequest request = new socalAPIRequest(url, form, callback, timeout, this);
        socalAPIDispatcher.Instance().addCallback( request );
        return true;
    }

    public override bool postMessageWithPhoto( mesageScope scope, string message, string phtotFilePath, int timeout,socalAPICallback callback){
        string url = "https://api.weibo.com/2/statuses/upload.json";
        WWWForm form = new WWWForm();
        form.AddField("access_token",m_accesstoken);
        form.AddField("status",message);
        int visible = 1;
        switch(scope){
            case socalAPI.mesageScope.mesageScope_public:
                visible = 0;
                break;
            case socalAPI.mesageScope.mesageScope_self:
                visible = 1;
                break;
            case socalAPI.mesageScope.mesageScope_myFriend:
                visible = 2;
                break;
        }
        form.AddField("visible",visible);
        byte[] fileData = loadFile(phtotFilePath);

        if ((fileData == null )||(fileData.Length ==0 ))
            return false;

        form.AddBinaryData("pic", fileData );

        socalAPIRequest request = new socalAPIRequest(url, form, callback, timeout, this);
        socalAPIDispatcher.Instance().addCallback( request );

        return true;
    }

    public class ReturnValue {
        public string request{
            get{
                return m_request;
            }
            set{
                m_request = value;
            }
        }

        public string error_code{
            get{
                return m_error_code;
            }
            set{
                m_error_code = value;
            }
        }

        public string error{
            get{
                return m_error;
            }
            set{
                m_error = value;
            }
        }
        string m_request;
        string m_error_code;
        string m_error;
    }

    public override socalAPIRequest.errorMessage parseError(string text){
        try{
            ReturnValue returnvalue = JsonMapper.ToObject<ReturnValue>(text);

            if (null != returnvalue)
            {
                if (returnvalue.error_code != null)
                {
                    socalAPIRequest.errorMessage error = new socalAPIRequest.errorMessage();
                    error.Message = String.Copy(returnvalue.error);
                    error.Code = System.Convert.ToInt32( returnvalue.error_code );
                    return error;
                }
            }
            return null;
        }
        catch{
            return null;
        }
    }

    public override bool isAuthFail(int error){
        switch (error){
        case 21319:
        case 21327:
            return true;
        default:
            return false;
        }
    }

    protected override bool serialize(string data , bool store )    {
        bool resule = base.serialize(data , store);
        if (resule){
            if (store){
                data += "uid,"+m_uid+",";
            }
            else{
                string[] separator = {","};
                string[] tokens = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                for(int i=0;i<tokens.Length;i++){
                    string key = (string)tokens.GetValue(i);
                    string value = (string)tokens.GetValue(++i);
                    if (key == "uid"){
                        m_uid = value;
                    }
                }
            }
        }
        return resule;
    }

    private string m_uid;
}

