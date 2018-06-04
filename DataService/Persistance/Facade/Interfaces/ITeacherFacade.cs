using Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Facade.Interfaces
{
    public interface ITeacherFacade
    {
        int AddTeacher(TeacherDto test);
        int UpdateTeacher(TeacherDto teacher, int teacherID);
    }
}
