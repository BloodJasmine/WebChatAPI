using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.ApiModels
{
    public class SendMessageApiModel
    {
        public string To { get; set; }
        public string Message { get; set; }
    }
}
