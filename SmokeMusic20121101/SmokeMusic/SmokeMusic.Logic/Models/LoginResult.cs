using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    /// <summary>
    /// 登录结果
    /// </summary>
    [DataContract]
    public class LoginResult
    {
        //{"err_no":1013,"r":1,"err_msg":"帐号和密码不匹配"}
        /// <summary>
        /// 错误编号
        /// </summary>
        [DataMember(Name="1013")]
        public int ErrorNo { get; set; }
        /// <summary>
        /// 是否发生错误,1代表发生了错误
        /// </summary>
        [DataMember(Name="r")]
        public int R { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember(Name="err_msg")]
        public string Message { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        [DataMember(Name="user_info")]
        public UserInfo UserInfo { get; set; }
    }
}
