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
        public void UsernameCounterDoesUpdate()
        {
            var userLogic = new UserLogic();
            var user = userLogic.CreateNewUser("testCustomer", "password123", UserLogic.AuthType.Customer);
            var lastChar = user.Username.Substring(user.Username.Length - 1, 1);
            Assert.IsTrue(int.TryParse(lastChar, out _));
        }

        [TestMethod]
        public void InvalidUserName()
        {
            var userLogic = new UserLogic();
            var result = userLogic.Login("bingo bob", "sillygoose");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InvalidPassword()
        {
            var userLogic = new UserLogic();
            var result = userLogic.Login("testAdmin", "sillygoose");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidUsername()
        {
            var userLogic = new UserLogic();
            var result = userLogic.Login("testAdmin", "password123");
            Assert.IsTrue(result);
        }
    }
}
