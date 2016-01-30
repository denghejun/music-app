using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Logic.Core
{
    public class Channel
    {
        public Models.ChannelList GetChannelList()
        {
            var conn = new ConnectionBase();
            var json = conn.Get("http://doubanfmcloud-channelinfo.stor.sinaapp.com/channelinfo");
            return Framework.Common.Helpers.JsonHelper.Deserialize<Models.ChannelList>(json);
        }
    }
}
