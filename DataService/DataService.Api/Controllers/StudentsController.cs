using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace DataService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IStudentRepository studentRepo;
        private readonly IUserRepository userRepo;

        public StudentsController(IStudentRepository studentRepo, IUserRepository userRepo)
        {
            this.studentRepo = studentRepo;
            this.userRepo = userRepo;
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            _log.Info("Get all the students.");

            return studentRepo.GetStudents();
        }

        // POST: api/Students
        [HttpPost]
        public int Post([FromBody]Student student)
        {
            _log.Info("Insert a new student: " + student.FirstName + " " + student.LastName);

            return studentRepo.AddOrUpdateStudent(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]Student student)
        {
            _log.Info("Update the student: " + student.FirstName + " " + student.LastName);

            return studentRepo.AddOrUpdateStudent(student, null, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var studentToDelete = studentRepo.GetStudents().First<Student>(student => student.StudentID == id);

            _log.Info("Delete the student: " + studentToDelete.FirstName + " " + studentToDelete.LastName);

            return userRepo.DeleteUser(studentToDelete.UserID);
        }
    }
}