using System;
using UnityEngine;

namespace com.softstar.unity.social
{
    public class SocialEvents
    {
        public delegate void Action();
        public static Action<string> onLogin = delegate {};
        public static Action onLogout = delegate {};
        public static Action<SocialUser> onRequestInfo = delegate {};
    }
}

