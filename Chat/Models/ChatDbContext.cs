using AuthModule.Model;
using Microsoft.EntityFrameworkCore;

namespace Chat.Models
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }
        public DbSet<Message> Message { set; get; }
        public DbSet<User> User { set; get; }
    }
}
