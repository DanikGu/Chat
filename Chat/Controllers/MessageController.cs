using AuthModule.Extensions;
using AuthModule.Model;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Message")]
    public class MessageController : Controller
    {
        private readonly ChatDbContext _context;
        
        public MessageController(ChatDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "User")]
        [Route("SendMessage")]
        public JsonResult SendMessage(SendedMessage message)
        {
            try
            {
                var mess = new Message()
                {
                    Text = message.message,
                    CreatedDateValue = DateTime.Now,
                    SenderId = Request.HttpContext.Session.Get<User>("User").Id,
                    RecipientId = message.recipientId
                };
                _context.Message.Add(mess);
                _context.SaveChangesAsync();
                return new JsonResult(new { Succsess = true });
            } catch (Exception ex) {
                return new JsonResult(new { Succsess = false, Message = ex.Message });
            }            
        }
        [Authorize(Roles = "User")]
        [Route("ReciveLastMessages")]
        public JsonResult ReciveLastMessages()
        {
            try
            {
                var userId = Request.HttpContext.Session.Get<User>("User").Id;
                return new JsonResult(
                    new { 
                        Succsess = true,
                        Messages = _context.Message.
                                        Where(mess => mess.SenderId == userId || mess.RecipientId == userId).
                                        AsEnumerable().
                                        OrderByDescending(mess=> mess.CreatedDateValue).
                                        Take(300).ToArray() 
                        });
            } catch (Exception ex) {
                return new JsonResult(new { Succsess = false, Message = ex.Message });
            }
        }
        [Authorize(Roles = "User")]
        [Route("ReciveMessages")]
        public JsonResult ReciveMessages(MessagesFilter filter)
        {
            try {
                var userId = Request.HttpContext.Session.Get<User>("User").Id;
                return new JsonResult(
                    new { 
                        Succsess = true,
                        Messages = _context.Message.
                                        OrderByDescending(mess => mess.CreatedDateValue).
                                        Where(mess => mess.SenderId      == (filter.SenderId ?? userId)&& 
                                                       mess.RecipientId   == (filter.RecipientId ?? userId) &&
                                                       (filter.SendSecond == null || (filter.SendSecond.Value.Second == mess.CreatedDateValue.Second)) &&
                                                       (filter.SendMinute == null || (filter.SendMinute.Value.Minute == mess.CreatedDateValue.Minute)) &&
                                                       (filter.SendHour   == null || (filter.SendHour.Value.Hour == mess.CreatedDateValue.Hour)) &&
                                                       (filter.SendDay    == null || (filter.SendDay.Value.Day == mess.CreatedDateValue.Day)) &&
                                                       (filter.SendMonth  == null || (filter.SendMonth.Value.Month == mess.CreatedDateValue.Month)) &&
                                                       (filter.SendYear   == null || (filter.SendYear.Value.Year == mess.CreatedDateValue.Year)) 
                                        ).
                                        Skip(filter.Skip ?? 0).
                                        Take(300).
                                        ToArray()
                        });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Succsess = false, Message = ex.Message });
            }
        }
    }
    public record SendedMessage(string message, int recipientId);
    public record MessagesFilter(int? SenderId, 
                                 int? RecipientId, 
                                 int? Skip, 
                                 DateTime? SendSecond, 
                                 DateTime? SendMinute, 
                                 DateTime? SendHour, 
                                 DateTime? SendDay, 
                                 DateTime? SendMonth, 
                                 DateTime? SendYear);
}
