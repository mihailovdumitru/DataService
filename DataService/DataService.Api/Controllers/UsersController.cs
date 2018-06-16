using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace DataService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IUserRepository userRepo;

        public UsersController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        [HttpGet]
        [Route("username/{username}")]
        public User Get(string username)
        {
            _log.Info("Get the user: " + username);

            return userRepo.GetUserByUsername(username);
        }

        // POST: api/Users
        [HttpPost]
        public int Post([FromBody]User user)
        {
            _log.Info("Insert a new user: " + user.Username);

            return userRepo.AddOrUpdateUser(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]User user)
        {
            _log.Info("Update the user: " + user.Username);

            return userRepo.AddOrUpdateUser(user, null, id);
        }
    }
}