using System.Collections.Generic;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.DBObjects;
using Persistance.Interfaces;

namespace DataService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Classes")]
    public class ClassesController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IClassRepository classRepo;

        public ClassesController(IClassRepository classRepo)
        {
            this.classRepo = classRepo;
        }

        // GET: api/Classes
        [HttpGet]
        public IEnumerable<StudyClass> Get()
        {
            _log.Info("Get all the classes.");

            return classRepo.GetClasses();
        }

        // POST: api/Classes
        [HttpPost]
        public int Post([FromBody]StudyClass studyClass)
        {
            _log.Info("Insert a new class: " + studyClass.Name);

            return classRepo.AddOrUpdateClass(studyClass);
        }

        // PUT: api/Classes/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]StudyClass studyClass)
        {
            _log.Info("Update the class: " + studyClass.Name);

            return classRepo.AddOrUpdateClass(studyClass, null, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _log.Info("Delete the class. ClassId: " + id);

            classRepo.DeleteClass(id);
        }
    }
}