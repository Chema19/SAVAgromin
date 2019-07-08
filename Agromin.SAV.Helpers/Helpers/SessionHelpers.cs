using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Agromin.SAV.Helpers.Helpers
{
    public enum AppRol
    {
        Administrador
    }

    public enum SessionKey
    {
        UserId,
        FullName,
        Rol,
        RolId,
        RolCompleto,
        Codigo,
        LocalId,
        Accesos,
        AplicacionId,
        Permisos,
        _appBP,
        LstRolUsuario,
        ViewPermisos,
    }
    public static class SessionHelpers
    {
        #region Private

        private static object Get(HttpSessionState Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionState Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionState Session, String Key)
        {
            return Session[Key] != null;
        }

        private static object Get(HttpSessionStateBase Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionStateBase Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionStateBase Session, String Key)
        {
            return Session[Key] != null;
        }

        #endregion

        #region Getters setters GlobalKey
        //HttpSessionState
        public static object Get(this HttpSessionState Session, SessionKey Key)
        {
            return Get(Session, Key.ToString());
        }

        public static void Set(this HttpSessionState Session, SessionKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionState Session, SessionKey Key)
        {
            return Exists(Session, Key.ToString());
        }

        //HttpSessionStateBase
        public static object Get(this HttpSessionStateBase Session, SessionKey Key)
        {
            //try
            //{
            return Get(Session, Key.ToString());
            //}
            //catch(Exception)
            //{
            //    return null;
            //}
        }

        public static void Set(this HttpSessionStateBase Session, SessionKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionStateBase Session, SessionKey Key)
        {
            return Exists(Session, Key.ToString());
        }
        #endregion

        #region Get_Full_Name

        public static String GetFullName(this HttpSessionState Session)
        {
            return (String)Get(Session, SessionKey.FullName);
        }

        public static String GetFullName(this HttpSessionStateBase Session)
        {
            return (String)Get(Session, SessionKey.FullName);
        }
        #endregion

        #region Get_UserId
        public static Int32 GetUserId(this HttpSessionState Session)
        {
            return Convert.ToInt32(Get(Session, SessionKey.UserId));
        }

        public static Int32 GetUserId(this HttpSessionStateBase Session)
        {
            return Convert.ToInt32(Get(Session, SessionKey.UserId));
        }
        #endregion

        #region Get_LocalId
        public static Int32 GetLocalId(this HttpSessionState Session)
        {
            return Convert.ToInt32(Get(Session, SessionKey.LocalId));
        }

        public static Int32 GetLocalId(this HttpSessionStateBase Session)
        {
            return Convert.ToInt32(Get(Session, SessionKey.LocalId));
        }
        #endregion
    }
}
