using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.ApiModels
{
    public class LoginResponse
    {
        public string error { get; set; }
        public string session_token { get; set; }
        public string user_id { get; set; }
    }
}
