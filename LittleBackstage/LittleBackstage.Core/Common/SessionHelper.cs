using System;
using System.Web;

namespace LittleBackstage.Core.Common
{
    public static class SessionHelper
    {
        public static string GetSessionId()
        {
            return HttpContext.Current.Session.SessionID;
        }

        public static void SetSession(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static string GetSession(string key)
        {
            var value = HttpContext.Current.Session[key];
            return value != null ? value.ToString() : String.Empty;
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public static void DeleteSession(string key)
        {
            HttpContext.Current.Session[key] = null;
        }

        public static void SetToken(string token)
        {
            SetSession("token", token);
        }

        public static string GetToken()
        {
            return GetSession("token");
        }
    }
}
