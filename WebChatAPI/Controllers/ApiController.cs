using WebChatAPI.ApiModels;
using WebChatAPI.Models;
using WebChatAPI.Models.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.Controllers
{

    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private ChatAPI chatAPI;
        private WebChatContext dbContext;

        public ApiController(WebChatContext context)
        {
            dbContext = context;
            chatAPI = new ChatAPI("https://testchat.doctorpeso.co/index.php/restapi/", "superadmin", "RBOqfNMkTlwPOVD4", dbContext);
        }

        [Route("send_message")]
        [HttpPost]
        public IActionResult SendMessage([FromForm] SendMessageApiModel formData)
        {
            return Ok(chatAPI.SendMessage(formData.To, formData.Message));
        }

        [Route("close_chat")]
        [HttpPost]
        public IActionResult CloseChat([FromForm] CloseChatApiModel formData)
        {
            return Ok(chatAPI.CloseChat(formData.To));
        }

        [Route("last_messages")]
        [HttpGet]
        public IActionResult GetLastMessages()
        {
            return Ok(chatAPI.LastMessages());
        }


        [Route("chat_messages/{chatid}")]
        [HttpGet]
        public IActionResult GetMessagesByChatId(int chatid)
        {
            return Ok(chatAPI.ChatMessagesById(chatid));
        }
    }
    
}
