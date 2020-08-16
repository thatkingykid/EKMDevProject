using System;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using System.Collections.Generic;

namespace BusinessLogicUnitTests
{
    [TestClass]
    public class UserLogicUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void InvalidAuthType()
        {
            var auths = new List<Auth>
            {
                new Auth{ID = 1, AuthType = "Admin" },
                new Auth{ID = 2, AuthType = "Customer" }
            };

            var efContext = EntityFrameworkGenerator.GenerateMockFramework(auths: auths);
            var userLogic = new UserLogic(efContext);
            userLogic.CreateNewUser("testAdmin", "password123", UserLogic.AuthType.TotallyNotRealAuthType);
        }

        [TestMethod]
        public void ValidAdminUser()
        {
            var auths = new List<Auth>
            {
                new Auth{ID = 1, AuthType = "Admin" },
                new Auth{ID = 2, AuthType = "Customer" }
            };

            var efContext = EntityFrameworkGenerator.GenerateMockFramework(auths: auths);
            var userLogic = new UserLogic(efContext);
            var user = userLogic.CreateNewUser("testAdmin", "password123", UserLogic.AuthType.Admin);
            Assert.IsTrue(user.Username.Contains("testAdmin") && user.Password == "password123");
        }

        [TestMethod]
        public void ValidCustomerUser()
        {
            var auths = new List<Auth>
            {
                new Auth{ID = 1, AuthType = "Admin" },
                new Auth{ID = 2, AuthType = "Customer" }
            };

            var efContext = EntityFrameworkGenerator.GenerateMockFramework(auths: auths);
            var userLogic = new UserLogic(efContext);
            var user = userLogic.CreateNewUser("testCustomer", "password123", UserLogic.AuthType.Customer);
            Assert.IsTrue(user.Username.Contains("testCustomer") && user.Password == "password123");
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void DuplicateUserName()
        {
            var users = new List<SysUser>
            {
                new SysUser{ID = 1, Username = "testCustomer", Password = "password" }
            };

            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users);
            var userLogic = new UserLogic(efContext);
            userLogic.CreateNewUser("testCustomer", "password123", UserLogic.AuthType.Customer);
        }

        [TestMethod]
        public void InvalidUserName()
        {
            var users = new List<SysUser>
            {
                new SysUser{ID = 1, Username = "testCustomer", Password = "password" }
            };

            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users);
            var userLogic = new UserLogic(efContext);
            var result = userLogic.Login("bingo bob", "sillygoose");
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void InvalidPassword()
        {
            var users = new List<SysUser>
            {
                new SysUser{ID = 1, Username = "testAdmin", Password = "password" }
            };

            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users);
            var userLogic = new UserLogic(efContext);
            var result = userLogic.Login("testAdmin", "sillygoose");
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void ValidLogin()
        {
            var users = new List<SysUser>
            {
                new SysUser{ID = 1, Username = "testAdmin", Password = "password123" }
            };

            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users);

            var userLogic = new UserLogic(efContext);
            var result = userLogic.Login("testAdmin", "password123");
            Assert.IsTrue(result == 1);
        }
    }
}
