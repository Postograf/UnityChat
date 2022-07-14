using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.Models;

namespace Server.Controllers
{

    public class MessageList
    {
        public IEnumerable<Message> Messages { get; set; }
    }

    [ApiController]
    [Route("api/messages")]
    public class MessageController : Controller
    {
        private ChatContext _db;

        public MessageController(ChatContext context)
        {
            _db = context;
        }

        [HttpGet("{count}")]
        public MessageList GetMessages(int count)
        {
            return new MessageList 
            { 
                Messages = _db
                    .Messages
                    .Include(m => m.Sender)
                    .OrderBy(m => m.Id)
                    .ToList()
                    .TakeLast(count)
            };
        }
    }
}
