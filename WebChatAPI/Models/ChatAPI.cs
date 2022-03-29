using WebChatAPI.ApiModels;
using WebChatAPI.Models.Database;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebChatAPI.Models
{
   
    public class ChatAPI
    {
        private string Path = null;
        private string UserName = null;
        private string ApiKey = null;
        private WebChatContext dbContext;

        public ChatAPI(string path, string username, string apikey, WebChatContext context)
        {
            Path = path;
            UserName = username;
            ApiKey = apikey;
            dbContext = context;
        }

        public string SendLastMessagesRequest(string json)
        {
            string url = "https://testscrm.doctorpeso.co/api/Chat/SetData";

            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6InByb2dyYW1tIiwiaWF0IjoxNTE2MjM5MDIyfQ.Se7s_Mi1JjBxfBdhvBiI64K9pS9mQ91JRJ7HM2yMlGQ");
            
            request.AddJsonBody(json);
               
            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        public string LastMessages()
        {
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

            string json = System.Text.Json.JsonSerializer.Serialize<SendLastMessagesApiModel>(apiModel);

            return json;
        }

        public IRestResponse SendRequest(string method, Dictionary<string,string> data = null, Method methodSend = Method.GET)
        {

            string url = Path + method;

            var client = new RestClient(url);
         
            var request = new RestRequest(methodSend);

            if (methodSend == Method.GET)
            {
                request.AddHeader("Authorization", "Basic c3VwZXJhZG1pbjoxcWFAV1MzZWQ=");
            }
            else if(methodSend == Method.POST)
            {
                request.AlwaysMultipartFormData = true;

                if (data != null)
                {
                    foreach (KeyValuePair<string, string> keyValue in data)
                    {
                        request.AddParameter(keyValue.Key, keyValue.Value);
                    }
                }
            }

            IRestResponse response = client.Execute(request);

            return response;
        }

        public string ChatMessagesById(int id)
        {
            SendLastMessagesApiModel messagesApiModel = new SendLastMessagesApiModel { TypeId = 6, ChatItems = new List<ChatItem>() };

            foreach (var msg in dbContext.Messages.Where(m => m.ChatId == id))
            {
                messagesApiModel.ChatItems.Add(new ChatItem
                {
                    body = msg.Msg,
                    chatid = msg.ChatId.ToString(),
                    date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToInt32(msg.Time)),
                    id = msg.Id.ToString(),
                    from = msg.UserId == "0" ? msg.ChatId.ToString() : "Admin",
                    to = msg.UserId == "0" ? "Admin" : msg.ChatId.ToString()
                });
            }

            string json = System.Text.Json.JsonSerializer.Serialize<SendLastMessagesApiModel>(messagesApiModel);

            return json;
        }
        public string SendMessage(string to, string message)
        {
            //Сохранение сообщения в базе
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == Convert.ToInt32(to));
            chat.LastMessageId = chat.LastMessageId + 1;

            //Текущее время timestamp
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.UtcNow.ToUniversalTime() - origin;
            var time = Math.Floor(diff.TotalSeconds);

            dbContext.Messages.Add(new WebChatAPI.Models.Database.Message 
            {
                Id = chat.LastMessageId,
                ChatId = Convert.ToInt32(to),
                Time = time.ToString(),
                Meta_Msg = "",
                Msg = message,
                UserId = "1",
                Name_support = "Admin"
            });

            dbContext.SaveChanges();


            var data = new Dictionary<string, string>()
            {
                {"chat_id", to },
                {"msg", message },
                {"sender", "operator" },
                {"operator_name", "Admin" }
            };

            var response = SendRequest("addmsguser", data, Method.POST);

            if (response.ResponseStatus == ResponseStatus.Completed || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = new Dictionary<string, string>
                {
                    {"status", "200"}
                };

                string json = System.Text.Json.JsonSerializer.Serialize<Dictionary<string, string>>(responseData);

                return json;
            }
            else
            {
                var responseData = new Dictionary<string, string>
                {
                    { "status", "400"}
                };

                string json = System.Text.Json.JsonSerializer.Serialize<Dictionary<string, string>>(responseData);

                return json;
            }
        }
        public string CloseChat(string chatId)
        {
            var chat = dbContext.Chats.FirstOrDefault(c => c.Id == Convert.ToInt32(chatId));
            chat.Status = 2;
            dbContext.SaveChanges();

            var data = new Dictionary<string, string>()
            {
                {"chat_id", chatId },
                {"status", "2" },
            };

            var response = SendRequest("setchatstatus", data, Method.POST);

            if (response.ResponseStatus == ResponseStatus.Completed || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = new Dictionary<string, string>
                {
                    { "status", "200"}
                };

                string json = System.Text.Json.JsonSerializer.Serialize<Dictionary<string, string>>(responseData);

                return json;
            }
            else
            {
                var responseData = new Dictionary<string, string>
                {
                    { "status", "400"}
                };

                string json = System.Text.Json.JsonSerializer.Serialize<Dictionary<string, string>>(responseData);

                return json;
            }
        }


        #region Login
        /*
        public string Login()
        {
            var data = new Dictionary<string, string>()
            {
                {"username", UserName },
                {"password", ApiKey },
                {"generate_token", "true" },
                {"device", "unknown" }
            };
           
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(SendRequest("login",data));
    
            return loginResponse.session_token;
        }
        */
        #endregion

        #region AllMessages
        /*
        public string AllChatsMessages()
        {
           
            string allMesages = "";

            //List<int> allChatsIds = new List<int>();

            ChatsResponse allChatsInfo = JsonConvert.DeserializeObject<ChatsResponse>(SendRequest("chats"));

            foreach (List list in allChatsInfo.list)
            {
                allMesages += ChatMessagesById(list.id.ToString());
            }
            
            return allMesages;
        }
    */
        #endregion

    }
}