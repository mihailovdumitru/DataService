using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Auth/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Auth
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Auth/5
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
