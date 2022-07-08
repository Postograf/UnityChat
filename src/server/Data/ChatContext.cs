using Microsoft.EntityFrameworkCore;

using Server.Models;

namespace Server.Data
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public string DbPath { get; set; }

        public ChatContext(DbContextOptions<ChatContext> optionsBuilder)
            : base(optionsBuilder) 
        {
            DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "chat.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
