using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.Models.Database
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Time { get; set; }
        public string Name_support { get; set; }
        public string Msg { get; set; }
        public string Meta_Msg { get; set; }
        public string UserId { get; set; }

        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
