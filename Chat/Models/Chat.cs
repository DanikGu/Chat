using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Models
{
    [Table("Chat")]
    public class Chat
    {
        public int Id {  get; set; }
        public string? Name {  get; set; }
        public IEnumerable<Message> Messages { set; get; }
        public IEnumerable<User> Users { set; get; }
        public IEnumerable<ChatUser> ChatUser { set; get; }
    }
}
