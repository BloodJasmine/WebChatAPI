using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.Models.Database
{
    public class Chat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int Status { get; set; }
        public int StatusUser { get; set; }
        public int LastMessageId { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
