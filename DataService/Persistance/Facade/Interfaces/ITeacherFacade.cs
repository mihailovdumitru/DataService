using Model.DTO;

namespace Persistance.Facade.Interfaces
{
    public interface ITeacherFacade
    {
        int AddTeacher(TeacherDto test);
        int UpdateTeacher(TeacherDto teacher, int teacherID);
    }
}