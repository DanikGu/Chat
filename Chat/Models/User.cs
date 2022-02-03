using AuthMiddlware.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace Chat.Models
{
    [Table("User")]
    public class User: AuthMiddlware.Model.User
    {
        public IEnumerable<Chat> Chats { get; set; }
        public IEnumerable<ChatUser> ChatUser { set; get; }
    }
}
