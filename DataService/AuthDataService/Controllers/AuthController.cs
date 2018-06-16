using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace AuthDataService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        public ITeacherRepository teacherRepo;

        public AuthController(ITeacherRepository teacherRepo)
        {
            this.teacherRepo = teacherRepo;
        }

        // GET: api/Auth
        [HttpGet]
        public Teacher TeacherAuth(string email)
        {
            return teacherRepo.GetTeacherUserAuth(email);
        }
    }
}