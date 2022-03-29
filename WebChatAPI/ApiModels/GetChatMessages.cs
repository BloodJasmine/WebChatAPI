using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.ApiModels
{
    public class Message
    {
        public string id { get; set; }
        public string msg { get; set; }
        public string meta_msg { get; set; }
        public string time { get; set; }
        public string chat_id { get; set; }
        public string user_id { get; set; }
        public string name_support { get; set; }
    }

    public class Result
    {
        public List<Message> messages { get; set; }
        public string ot { get; set; }
        public bool closed { get; set; }
        public bool check_status { get; set; }
        public int lmid { get; set; }
    }

    public class GetChatMessages
    {
        public bool error { get; set; }
        public Result result { get; set; }
    }
}
