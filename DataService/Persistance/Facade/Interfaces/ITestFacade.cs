using Model.DTO.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Facade.Interfaces
{
    public interface ITestFacade
    {
        int AddTestObject(TestModelDto test);

        int UpdateTest(TestModelDto test);
        TestModelDto GetTestObject(int id);
    }
}
