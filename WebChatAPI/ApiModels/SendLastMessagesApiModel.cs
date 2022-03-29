using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.ApiModels
{
    public class ChatItem
    {
        public string id { get; set; }
        public string chatid { get; set; }
        public DateTime date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string body { get; set; }
    }

    public class SendLastMessagesApiModel
    {
        public int TypeId { get; set; }
        public List<ChatItem> ChatItems { get; set; }
    }
}
