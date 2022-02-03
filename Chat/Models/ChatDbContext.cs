using Microsoft.EntityFrameworkCore;

namespace Chat.Models
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Chat>()
                .HasMany(p => p.Users)
                .WithMany(p => p.Chats)
                .UsingEntity<ChatUser>(
                    j => j
                        .HasOne(pt => pt.User)
                        .WithMany(t => t.ChatUser)
                        .HasForeignKey(pt => pt.UserId),
                    j => j
                        .HasOne(pt => pt.Chat)
                        .WithMany(p => p.ChatUser)
                        .HasForeignKey(pt => pt.ChatId),
                    j =>
                    {
                        j.HasKey(t => new { t.ChatId, t.UserId });
                    });
        }
        public DbSet<Message> Message { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Chat> Chats {  get; set; }
    }
}
