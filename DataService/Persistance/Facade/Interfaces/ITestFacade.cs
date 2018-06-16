using Model.DTO.Test;

namespace Persistance.Facade.Interfaces
{
    public interface ITestFacade
    {
        int AddTestObject(TestModelDto test);

        int UpdateTest(TestModelDto test);
        TestModelDto GetTestObject(int id);
    }
}