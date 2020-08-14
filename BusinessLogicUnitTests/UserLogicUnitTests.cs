using System;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogicUnitTests
{
    /// <summary>
    /// Unit test class for UserLogic from the Business Logic project. In a real
    /// dev environment, I would use Moq to create a fake Entity Framework client
    /// to avoid writing dummy data into the db.
    /// </summary>
    
    [TestClass]
    public class UserLogicUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void InvalidAuthType()
        {
            var userLogic = new UserLogic();
            userLogic.CreateNewUser("testAdmin", "password123", UserLogic.AuthType.TotallyNotRealAuthType);
        }

        [TestMethod]
        public void ValidAdminUser()
        {
            var userLogic = new UserLogic();
            var user = userLogic.CreateNewUser("testAdmin", "password123", UserLogic.AuthType.Admin);
            Assert.IsTrue(user.ID != 0 && user.Username.Contains("testAdmin") && user.Password == "password123");
        }

        [TestMethod]
        public void ValidCustomerUser()
        {
            var userLogic = new UserLogic();
            var user = userLogic.CreateNewUser("testCustomer", "password123", UserLogic.AuthType.Customer);
            Assert.IsTrue(user.ID != 0 && user.Username.Contains("testCustomer") && user.Password == "password123");
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void DuplicateUserName()
        {
            var userLogic = new UserLogic();
            userLogic.CreateNewUser("testCustomer", "password123", UserLogic.AuthType.Customer);
        }

        [TestMethod]
        public void InvalidUserName()
        {
            var userLogic = new UserLogic();
            var result = userLogic.Login("bingo bob", "sillygoose");
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void InvalidPassword()
        {
            var userLogic = new UserLogic();
            var result = userLogic.Login("testAdmin", "sillygoose");
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void ValidUsername()
        {
            var userLogic = new UserLogic();
            var result = userLogic.Login("testAdmin", "password123");
            Assert.IsTrue(result == 0);
        }
    }
}
