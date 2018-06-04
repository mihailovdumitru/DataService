using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public StudentsController(IStudentRepository studentRepo)
        {
            this.studentRepo = studentRepo;
        }
        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return studentRepo.GetStudents();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        public void Delete(int id)
        {
        }
    }
}
