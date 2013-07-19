using System;
using UnityEngine;
using System.Collections;

public class socalAPIRequest
{
    public class errorMessage {
        public string Message;
        public int Code;
        public errorMessage(){
            Code = 0;
        }
    }

    public socalAPIRequest (string url, byte[] formData, Hashtable headers , socalAPICallback callback, int timeout, socalAPI api)
    {
        m_API = api;
        m_cb = callback;
        if (null != callback){
            callback.request = this;
        }

        string value = (string)headers["Content-Type"];

        if (value.Contains("boundary"))
        {
            // remove "
            int start = value.IndexOf("boundary");

            char[] temp = value.ToCharArray();

            for (int i=start;i<value.Length;i++)
            {
                if (34 == temp[i])
                {
                    Array.Copy(temp,i+1,temp,i,value.Length - (i + 1));
                    temp[value.Length-1] = '\0';
                }
            }
            value = new string(temp);
            headers["Content-Type"] = value;
        }
//        value += ";";
//        headers["Content-Type"] = value;
//        byte[] newformData = new byte[formData.Length - 2];
//        Array.Copy(formData,2,newformData,0,formData.Length - 2 );
        m_www = new WWW(url, formData , headers);

        m_duration = timeout * 1000;
        m_currentduration = 0;
    }

    public socalAPIRequest (string url, WWWForm form, socalAPICallback callback, int timeout, socalAPI api)
    {
        m_API = api;

        m_cb = callback;
        if (null != callback){
            callback.request = this;
        }
        if (null == form){
            m_www = new WWW(url);
        }
        else{
            m_www = new WWW(url, form);
        }

        m_duration = timeout * 1000;
        m_currentduration = 0;
    }

    // property

    public bool isDone {
        get{
            if (null == m_www)
                return true;
            return m_www.isDone;
        }
    }

    public bool isTimeout{
        get{
            if (m_duration > 0){
                return (m_duration <= m_currentduration);
            }
            else{
                return false;
            }
        }
    }

    public bool isError{
        get{
            if (null != m_www){
                if (false == String.IsNullOrEmpty(m_www.error))
                {
                    m_errorMessage = new errorMessage();
                    m_errorMessage.Message = String.Copy(m_www.error);
                    m_errorMessage.Code = 0;
                    return true;
                }
                else{
                    if (m_www.isDone)
                    {
                        m_errorMessage = m_API.parseError(m_www.text );

                        if (null != m_errorMessage){
                            return (false == String.IsNullOrEmpty(m_errorMessage.Message));
                        }
                    }
                    m_errorMessage = new errorMessage();
                    return false;
                }
            }
            else{
                return false;
            }
        }
    }

    public bool isAuthFail{
        get{
            if (null != m_errorMessage){
                return m_API.isAuthFail(m_errorMessage.Code);
            }
            return false;
        }
    }
    public void update(long deltaTime){
        if (m_duration > 0)
        {
            if (m_duration > m_currentduration)    {
                m_currentduration += deltaTime;
            }
        }
    }

    public void onCompleted(string result){
        if (null != m_cb){

            m_cb.onCompleted(result);
        }
    }

    public void onError(){
        // because i can not get any error message to determine auth-fail
        if (((m_API.className == "facebook")||(m_API.className == "sina_weibo"))&& (m_errorMessage.Code == 0))
        {
            onAuthFail();
        }
        else{
            if (null != m_cb){
                m_cb.onError(m_errorMessage.Message);
            }
        }
    }

    public void onAuthFail(){
        if (null != m_cb){
            m_API.releaseAccessToken();
            m_cb.onAuthFail();
        }
    }

    public void onTimeout(){
        if (null != m_cb){
            m_cb.onTimeout();
        }
    }

    public string Result {
        get {
            return m_www.text;
        }
    }

    WWW m_www;
    socalAPICallback m_cb;
    long m_duration;
    long m_currentduration;

    socalAPI m_API;
    errorMessage m_errorMessage;
}

