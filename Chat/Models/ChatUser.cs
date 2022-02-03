using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Models
{
    [Table("ChatUser")]
    public class ChatUser
    {
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
