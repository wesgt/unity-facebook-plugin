using System;
using UnityEngine;
public abstract class socalAPICallback
{
    public socalAPICallback ()
    {

    }

    public socalAPIRequest request{
        get {
            return m_request;
        }
        set {
            m_request = value;
        }
    }
    public abstract void onCompleted(string text);
    public abstract void onTimeout();
    public abstract void onError(string error);
    public abstract void onAuthFail();

    socalAPIRequest m_request;
}

