using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.ApiModels
{
    public class WebhookApiModel
    {
        public string id { get; set; }
        public string time { get; set; }
        public string chat_id { get; set; }
        public string user_id { get; set; }
        public string name_support { get; set; }
        public string msg { get; set; }
        public string meta_msg { get; set; }


        public string status { get; set; }
        public string status_user { get; set; }
        public string last_msg_id { get; set; }
    }
}
