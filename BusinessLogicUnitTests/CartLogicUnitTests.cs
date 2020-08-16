using BusinessLogic;
using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicUnitTests
{
    [TestClass]
    public class CartLogicUnitTests
    {
        public Guid TestSession;

        [TestMethod]
        public void CreateNewCartSession_Test()
        {
            var efContext = EntityFrameworkGenerator.GenerateMockFramework();
            var cartLogic = new CartLogic(1, efContext);
            var guid = cartLogic.CreateNewCartSession();
            Assert.IsTrue(string.IsNullOrEmpty(guid.ToString()) == false);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void AddNewCartItem_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items);
            var cartLogic = new CartLogic(1, efContext);
            cartLogic.AddNewCartItem(new Guid(), 10000, 1);
        }

        [TestMethod]
        public void AddNewCartItem_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items);
            var cartLogic = new CartLogic(1, efContext);
            var cart = cartLogic.AddNewCartItem(new Guid(), 1, 1);
            Assert.IsTrue(cart != null);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void RemoveItem_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            cartLogic.RemoveItem(sessionGuid, 10000);
        }

        [TestMethod]
        public void RemoveItem_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            var stockToRetain = cartLogic.RemoveItem(sessionGuid, 1);
            Assert.IsTrue(stockToRetain == 1);
        }


        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void IncreaseItemQuantityInCart_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            cartLogic.IncreaseItemQuantityInCart(sessionGuid, 10000, 1);
        }

        [TestMethod]
        public void IncreaseItemQuantityInCart_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            var newStock = cartLogic.IncreaseItemQuantityInCart(sessionGuid, 1, 1);
            Assert.IsTrue(newStock == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void DecreaseItemQuantityInCart_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            cartLogic.DecreaseItemQuantityInCart(sessionGuid, 10000, 1);
        }

        [TestMethod]
        public void DecreaseItemQuantityInCart_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 2, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            var newStock = cartLogic.DecreaseItemQuantityInCart(sessionGuid, 1, 1);
            Assert.IsTrue(newStock == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]

        public void GetAllCartsForSession_InvalidSession()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            cartLogic.GetAllCartsForSession(new Guid());
        }

        [TestMethod]
        public void GetAllCartsForSession_ValidSession()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            var cartCount = cartLogic.GetAllCartsForSession(sessionGuid);
            Assert.IsTrue(cartCount.Count > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]

        public void AbandonSession_InvalidSession()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            cartLogic.GetAllCartsForSession(new Guid());
        }

        [TestMethod]
        public void AbandonSession_ValidSession()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            cartLogic.AbandonSession(sessionGuid);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void CreateOrderFromCarts_InvalidSession()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            cartLogic.CreateOrderFromCarts(new Guid());
        }

        [TestMethod]
        public void CreateOrderFromCarts_ValidSession()
        {
            var items = new List<Item>
            {
                new Item{ID= 1, Name = "TestItem",  CommonCode = "TEST", Price = 0, Stock = 0 }
            };

            var users = new List<SysUser>
            {
                new SysUser { ID = 1, Username = "testUser", Password = "Password" }
            };

            var sessionGuid = new Guid();
            var carts = new List<Cart>
            {
                new Cart{ID = 1, SessionID = sessionGuid, ItemID = 1, Quantity = 1, UserID = 1 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(users: users, items: items, carts: carts);
            var cartLogic = new CartLogic(1, efContext);
            var order = cartLogic.CreateOrderFromCarts(sessionGuid);
            Assert.IsTrue(order != null);
        }
    }
}
