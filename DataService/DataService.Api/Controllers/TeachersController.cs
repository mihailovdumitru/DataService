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
        public TeachersController(ITeacherFacade teacherFacade)
        {
            this.teacherFacade = teacherFacade;
        }
        // GET: api/Teachers
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
