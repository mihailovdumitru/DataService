using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        private readonly ITeacherFacade teacherFacade;
        private readonly ITeacherRepository teacherRepo;
        public TeachersController(ITeacherFacade teacherFacade, ITeacherRepository teacherRepo)
        {
            this.teacherFacade = teacherFacade;
            this.teacherRepo = teacherRepo;
        }
        // GET: api/Teachers
        [HttpGet]
        public IEnumerable<TeacherDto> Get()
        {
            //return teacherRepo.GetTeachersWithLectures();
            return teacherRepo.GetTeachers();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Teachers
        [HttpPost]
        public int Post([FromBody]TeacherDto teacher)
        {
            return teacherFacade.AddTeacher(teacher);
        }
        
        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]TeacherDto teacher)
        {
            return teacherFacade.UpdateTeacher(teacher, id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
