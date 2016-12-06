using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBackstage.Core.Common;
using LittleBackstage.Core.Models;
using LittleBackstage.Infrastructure.Services;

namespace LittleBackstage.Core.Basis
{
    public class UserLogin
    {
        private static readonly IHelperServices Help = new HelperServices();

        /// <summary>
        /// 判断是否已经登录(解决Session超时问题)
        /// </summary>
        private static bool IsUserLogin(string SESSION_USER_INFO)
        {
            //如果Session为Null
            if (Help.GetSessionObject(SESSION_USER_INFO) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取得会员信息
        /// </summary>
        public static Manager GetUserInfo(string SESSION_USER_INFO)
        {
            if (!IsUserLogin(SESSION_USER_INFO))
            {
                return null;
            }

            var model = Help.GetSessionObject(SESSION_USER_INFO) as Manager;
            return model;
        }

        /// <summary>
        /// 清除记录信息 清掉session
        /// </summary>
        public void ExitSign()
        {
            SessionHelper.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SESSION_ADMIN_PERMISSIONS"></param>
        /// <returns></returns>
        public static string GetPermission(string SESSION_ADMIN_PERMISSIONS)
        {
            if (System.Web.HttpContext.Current.Session[SESSION_ADMIN_PERMISSIONS] != null)
            {
                string permissions = ((string)System.Web.HttpContext.Current.Session[SESSION_ADMIN_PERMISSIONS]);
                if (permissions != null)
                {
                    return permissions;
                }
            }
            return null;
        }
        public static string[] GetPermissions(string SESSION_ADMIN_PERMISSIONS)
        {
            if (System.Web.HttpContext.Current.Session[SESSION_ADMIN_PERMISSIONS] != null)
            {

                string[] permissions = ((string)System.Web.HttpContext.Current.Session[SESSION_ADMIN_PERMISSIONS]).Split(',');
                if (permissions != null)
                {
                    return permissions;
                }
            }
            return null;
        }
    }
}
