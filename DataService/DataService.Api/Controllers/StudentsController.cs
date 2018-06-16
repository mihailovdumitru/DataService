using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace DataService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : Controller
    {
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
            return studentRepo.GetStudents();
        }

        // POST: api/Students
        [HttpPost]
        public int Post([FromBody]Student student)
        {
            return studentRepo.AddOrUpdateStudent(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]Student student)
        {
            return studentRepo.AddOrUpdateStudent(student, null, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var studentToDelete = studentRepo.GetStudents().First<Student>(student => student.StudentID == id);

            return userRepo.DeleteUser(studentToDelete.UserID);
        }
    }
}