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
    [Route("/")]
    public class HomeController: ControllerBase
    {
        private WebChatContext dbContext;
        public HomeController(WebChatContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ChatAPI chatAPI = new ChatAPI("http://testchat.doctorpeso.co/index.php/restapi/","superadmin", "RBOqfNMkTlwPOVD4",dbContext);
            //string token = chatAPI.Login();
            return Ok("Главная страница");
            //return Ok(token);
        }
    }
}
