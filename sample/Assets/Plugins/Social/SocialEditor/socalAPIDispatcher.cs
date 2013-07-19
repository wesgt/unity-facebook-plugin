using UnityEngine;
using System.Collections;
using System;
public class socalAPIDispatcher : MonoBehaviour {
    private static socalAPIDispatcher singleton = null;
    private static GameObject singletonGameObject = null;
    private static object singletonLock = new object();
    int m_lastruntime;
    socalAPIDispatcher()
    {
        m_lastruntime = System.Environment.TickCount;
        Debug.Log("StartTime:"+m_lastruntime);
    }

    public static socalAPIDispatcher Instance(){
        return singleton;
    }

    public static void Init(){
        lock(singletonLock){
            if (null == singleton){
                singletonGameObject = new GameObject();
                singleton = singletonGameObject.AddComponent<socalAPIDispatcher>();
                singletonGameObject.name = "socalAPIDispatcher";
                DontDestroyOnLoad(singletonGameObject);
            }
        }
    }

    protected ArrayList m_callbacks = ArrayList.Synchronized( new ArrayList() );

    public void addCallback(socalAPIRequest request){
        m_callbacks.Add(request );
    }

    void Start(){
        Debug.Log("socalAPIDispatcher::Start()");
    }

    // Update is called once per frame
    void Update () {
        int now = System.Environment.TickCount;
        int deltaTime = now - m_lastruntime;
        ArrayList freeRequest = new ArrayList();
        //Debug.Log("deltaTime:"+deltaTime);
        foreach(socalAPIRequest callback in m_callbacks)
        {
            callback.update(deltaTime);

            if (callback.isDone){
                try{
                    if (callback.isError){
                        if (callback.isAuthFail){
                            callback.onAuthFail();
                        }
                        else{
                            callback.onError();
                        }
                    }
                    else{
                        callback.onCompleted(callback.Result);
                    }
                    freeRequest.Add(callback);
                }
                catch(Exception e){
                    Debug.Log(e.Message);
                    freeRequest.Add(callback);
                }
            }
            else if (callback.isTimeout){
                Debug.Log("Timeout:"+now + " deltaTime:" + deltaTime);
                callback.onTimeout();
                freeRequest.Add(callback);
            }
        }

        foreach (socalAPIRequest request in freeRequest){
            m_callbacks.Remove(request);
        }

        m_lastruntime = now;
    }

    public void RefreshLastRunTime(){
        m_lastruntime = System.Environment.TickCount;
    }
}
