using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SlutProjekt2k19.Models
{
    public class Message
    {
        [Key] public string Id { get; set; }
        public string MessageSent { get; set; }
        public string Author { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string MessagePic { get; set; }
    }

    public class MessageDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessageDbContext() : base("DefaultConnection")
        {
        }
    }
}