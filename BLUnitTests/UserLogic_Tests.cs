using System;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLUnitTests
{
    /// <summary>
    /// Unit test for UserLogic from BusinessLogic project. Normally in a real dev environment I would use Moq framework to pass a fake Entity Framework
    /// client into the logic class, but due to the nature of the project the unit tests run data into the database
    /// </summary>


    [TestClass]
    public class UserLogic_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void InvalidAuthType()
        {
            var userLogic = new UserLogic();
            userLogic.CreateNewUser("testAdmin", "Password123", UserLogic.AuthType.TotallyNotRealAuthType);
        }
    }
}
