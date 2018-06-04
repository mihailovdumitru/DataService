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
    [Route("api/Lectures")]
    public class LecturesController : Controller
    {
        private readonly ILectureRepository lectureRepo;

        public LecturesController(ILectureRepository lectureRepo)
        {
            this.lectureRepo = lectureRepo;
        }


        // GET: api/Lectures
        [HttpGet]
        public IEnumerable<Lecture> Get()
        {
            return lectureRepo.GetLectures();
        }

        // GET: api/Lectures/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Lectures
        [HttpPost]
        public int Post([FromBody]Lecture lecture)
        {
            return lectureRepo.AddOrUpdate(lecture);
        }
        
        // PUT: api/Lectures/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]Lecture lecture)
        {
            return lectureRepo.AddOrUpdate(lecture, null, id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
