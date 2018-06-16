using System.Collections.Generic;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace DataService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Lectures")]
    public class LecturesController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ILectureRepository lectureRepo;

        public LecturesController(ILectureRepository lectureRepo)
        {
            this.lectureRepo = lectureRepo;
        }

        // GET: api/Lectures
        [HttpGet]
        public IEnumerable<Lecture> Get()
        {
            _log.Info("Get all the lectures.");

            return lectureRepo.GetLectures();
        }

        // POST: api/Lectures
        [HttpPost]
        public int Post([FromBody]Lecture lecture)
        {
            _log.Info("Insert a new lecture: " + lecture.Name);

            return lectureRepo.AddOrUpdate(lecture);
        }

        // PUT: api/Lectures/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]Lecture lecture)
        {
            _log.Info("Update the lecture: " + lecture.Name);

            return lectureRepo.AddOrUpdate(lecture, null, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _log.Info("Delete the lecture. LectureId: " + id);

            return lectureRepo.DeleteLecture(id);
        }
    }
}