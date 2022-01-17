using AuthModule.Model;

namespace Chat.Models
{
    public class Message
    {
        public int Id {  get; set; }
        public string Text {  get; set; }
        public string CreatedDate { get; set; }
        public DateTime CreatedDateValue {
            get {
                return DateTime.Parse(CreatedDate);
            }
            set {
                CreatedDate = value.ToLongDateString();
            }
        }
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
        public int RecipientId { get; set; }
        public virtual User Recipient { get; set; }
    }
}
