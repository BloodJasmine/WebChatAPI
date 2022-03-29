using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.Models.Database
{
    public class WebChatContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public WebChatContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=fadeevdb;Username=fadeev;Password=1qa@WS");
        }

        public WebChatContext(DbContextOptions<WebChatContext> options)
            : base(options)
        {

        }
    }
}

