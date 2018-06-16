using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;

namespace DataService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Teachers")]
    public class TeachersController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ITeacherFacade teacherFacade;
        private readonly ITeacherRepository teacherRepo;
        private readonly IUserRepository userRepo;

        public TeachersController(ITeacherFacade teacherFacade, ITeacherRepository teacherRepo, IUserRepository userRepo)
        {
            this.teacherFacade = teacherFacade;
            this.teacherRepo = teacherRepo;
            this.userRepo = userRepo;
        }

        // GET: api/Teachers
        [HttpGet]
        public IEnumerable<TeacherDto> Get()
        {
            _log.Info("Get all the teachers.");

            return teacherRepo.GetTeachers();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public TeacherDto Get(int id)
        {
            _log.Info("Get the teacher. TeacherId: " + id);

            return teacherRepo.GetTeachers().First(teacher => teacher.TeacherID == id);
        }

        // POST: api/Teachers
        [HttpPost]
        public int Post([FromBody]TeacherDto teacher)
        {
            _log.Info("Insert a new teacher: " + teacher.FirstName + " " + teacher.LastName);

            return teacherFacade.AddTeacher(teacher);
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]TeacherDto teacher)
        {
            _log.Info("Update the teacher: " + teacher.FirstName + " " + teacher.LastName);

            return teacherFacade.UpdateTeacher(teacher, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var teacherToDelete = teacherRepo.GetTeachers().First(teacher => teacher.TeacherID == id);

            _log.Info("Delete the teacher: " + teacherToDelete.FirstName + " " + teacherToDelete.LastName);

            return userRepo.DeleteUser(teacherToDelete.UserID);
        }
    }
}