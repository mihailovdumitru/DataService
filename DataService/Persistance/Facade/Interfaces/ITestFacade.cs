using Model.DTO.Test;

namespace Persistance.Facade.Interfaces
{
    public interface ITestFacade
    {
        int AddTestObject(TestModelDto test);
        TestModelDto GetTestObject(int id);
    }
}