
using WebChatAPI.ApiModels;
using WebChatAPI.Models;
using WebChatAPI.Models.Database;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebChatAPI.Controllers
{
    
    [ApiController]
    [EnableCors]
    [Route("/webhook")]
    public class WebhookController : ControllerBase
    {
        private WebChatContext dbContext;
        private ChatAPI chatAPI;
        public WebhookController(WebChatContext context)
        {
            dbContext = context;
            chatAPI = new ChatAPI("http://192.168.158.3/index.php/restapi/", "superadmin", "RBOqfNMkTlwPOVD4", context);
        }

        [HttpPost]
        public IActionResult Post([FromForm] WebhookApiModel formData)
        {
            if (dbContext.Chats.FirstOrDefault(c => c.Id.ToString() == formData.chat_id) == null)
            {
                dbContext.Chats.Add(new Chat
                {
                    Id = Convert.ToInt32(formData.chat_id),
                    Status = Convert.ToInt32(formData.status),
                    StatusUser = Convert.ToInt32(formData.status_user),
                    LastMessageId = Convert.ToInt32(formData.last_msg_id)
                });
                dbContext.SaveChanges();
            }
            else 
            {
                var chat = dbContext.Chats.FirstOrDefault(c => c.Id.ToString() == formData.chat_id);
                chat.Status = Convert.ToInt32(formData.status);
                chat.StatusUser = Convert.ToInt32(formData.status_user);
                chat.LastMessageId = Convert.ToInt32(formData.last_msg_id);
                dbContext.SaveChanges();
            }

            dbContext.Messages.Add(
                new Models.Database.Message
                {
                    Id = Convert.ToInt32(formData.id),
                    Time = formData.time,
                    Msg = formData.msg,
                    ChatId = Convert.ToInt32(formData.chat_id),
                    Meta_Msg = formData.meta_msg,
                    Name_support = formData.name_support,
                    UserId = formData.user_id
                }
            );
            dbContext.SaveChanges();

            SendLastMessagesApiModel apiModel = new SendLastMessagesApiModel()
            {
                TypeId = 6,
                ChatItems = new List<ChatItem>()
            };

            foreach (var chat in dbContext.Chats.Where(c => c.Status == 1))
            {
                var lastMessage = dbContext.Messages.FirstOrDefault(m => m.Id == chat.LastMessageId);
                apiModel.ChatItems.Add(new ChatItem
                {
                    body = lastMessage.Msg,
                    chatid = lastMessage.ChatId.ToString(),
                    date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToInt32(lastMessage.Time)),
                    id = lastMessage.Id.ToString(),
                    from = lastMessage.UserId == "0" ? lastMessage.ChatId.ToString() : "Admin",
                    to = lastMessage.UserId == "0" ? "Admin" : lastMessage.ChatId.ToString()
                });
            }

            string json = JsonSerializer.Serialize<SendLastMessagesApiModel>(apiModel);

            var response = chatAPI.SendLastMessagesRequest(json);

            return Ok();
        }
    }
    
}
