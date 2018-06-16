using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace AuthDataService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ITeacherRepository teacherRepo;

        public AuthController(ITeacherRepository teacherRepo)
        {
            this.teacherRepo = teacherRepo;
        }

        // GET: api/Auth
        [HttpGet]
        public Teacher TeacherAuth(string email)
        {
            _log.Info("Get teacher user auth: " + email);

            return teacherRepo.GetTeacherUserAuth(email);
        }
    }
}