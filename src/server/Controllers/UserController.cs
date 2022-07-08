using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private ChatContext _db;

        public UserController(ChatContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }
    }
}
