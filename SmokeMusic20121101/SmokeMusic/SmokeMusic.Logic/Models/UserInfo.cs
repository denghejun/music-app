using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    [DataContract]
    public class UserInfo
    {
        /// <summary>
        /// 用户主页
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 用于注销的字符串
        /// </summary>
        [DataMember(Name = "ck")]
        public string LogOffToken { get; set; }

        /// <summary>
        /// 播放记录
        /// </summary>
        [DataMember(Name = "play_record")]
        public PlayRecord PlayRecord { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
