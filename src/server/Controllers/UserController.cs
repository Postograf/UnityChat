using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    public class UserList
    {
        public IEnumerable<User>? Users { get; set; }
    }

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
        public UserList GetAll()
        {
            return new UserList { Users = _db.Users };
        }
    }
}
