using Framework.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmokeMusic.Logic.Core
{
    /// <summary>
    /// 账户操作类
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="captcha"></param>
        /// <param name="captchaID"></param>
        /// <param name="remember"></param>
        public Models.LoginResult Login(string userName, string password, string captcha, string captchaID, bool remember)
        {
            Parameters parameters = new Parameters();
            parameters["source"] = "radio";
            parameters["alias"] = userName;
            parameters["form_password"] = password;
            parameters["captcha_solution"] = captcha;
            parameters["captcha_id"] = captchaID;
            parameters["remember"] = "on";
            string json = new ConnectionBase().Post("http://douban.fm/j/login", Encoding.UTF8.GetBytes(parameters.ToString()));
            var result = Framework.Common.Helpers.JsonHelper.Deserialize<Models.LoginResult>(json);
            return result;
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user"></param>
        public void SaveUserInfo(Models.UserInfo user)
        {
            if (user == null) return;
            BinarySerializeHelper.Serialize(Paths.UserInfoFile, user);
        }
        /// <summary>
        /// 从本地文件得到用户信息
        /// </summary>
        /// <returns></returns>
        public Logic.Models.UserInfo GetUserInfo()
        {
            try
            {
                return BinarySerializeHelper.Deserialize<Logic.Models.UserInfo>(Paths.UserInfoFile);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 删除保存的用户信息
        /// </summary>
        public void ClearUserInfo()
        {
            try
            {
                File.Delete(Paths.UserInfoFile);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 保存登录的Cookies
        /// </summary>
        public void SaveCookies()
        {
            ConnectionBase.SaveCookies();
        }
        /// <summary>
        /// 删除Cookies
        /// </summary>
        public void ClearCookies()
        {
            try
            {
                File.Delete(Paths.CookiesFile);
            }
            catch
            {
 
            }
        }
    }
}
