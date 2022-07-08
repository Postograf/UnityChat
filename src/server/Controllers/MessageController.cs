using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Server.Data;
using Server.Models;

namespace Server.Controllers
{
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
        public IEnumerable<Message> GetMessages(int count)
        {
            return _db.Messages.OrderByDescending(x => x.Id).Take(count);
        }
    }
}
