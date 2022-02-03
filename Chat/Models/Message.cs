using AuthMiddlware.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Models
{
    public class Message
    {
        public int Id {  get; set; }
        public string Text {  get; set; }
        public string CreatedDate { get; set; }
        [NotMapped]
        public DateTime CreatedDateValue {
            get {
                return DateTime.Parse(CreatedDate);
            }
            set {
                CreatedDate = value.ToString();
            }
        }
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
