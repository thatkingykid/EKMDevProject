using BusinessLogic;
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
            var cartLogic = new CartLogic(1);
            TestSession = cartLogic.CreateNewCartSession();
            Assert.IsTrue(string.IsNullOrEmpty(TestSession.ToString()) == false);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void AddNewCartItem_InvalidItem()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.AddNewCartItem(TestSession, 10000, 1);
        }

        [TestMethod]
        public void AddNewCartItem_ValidItem()
        {
            var cartLogic = new CartLogic(1);
            var cart = cartLogic.AddNewCartItem(TestSession, 1, 1);
            Assert.IsTrue(cart != null);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void RemoveItem_InvalidItem()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.RemoveItem(TestSession, 10000);
        }

        [TestMethod]
        public void RemoveItem_ValidItem()
        {
            var cartLogic = new CartLogic(1);
            var stockToRetain = cartLogic.RemoveItem(TestSession, 1);
            Assert.IsTrue(stockToRetain == 1);
        }


        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void IncreaseItemQuantityInCart_InvalidItem()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.IncreaseItemQuantityInCart(TestSession, 10000, 1);
        }

        [TestMethod]
        public void IncreaseItemQuantityInCart_ValidItem()
        {
            var cartLogic = new CartLogic(1);
            var newStock = cartLogic.IncreaseItemQuantityInCart(TestSession, 1, 1);
            Assert.IsTrue(newStock == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void DecreaseItemQuantityInCart_InvalidItem()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.DecreaseItemQuantityInCart(TestSession, 10000, 1);
        }

        [TestMethod]
        public void DecreaseItemQuantityInCart_ValidItem()
        {
            var cartLogic = new CartLogic(1);
            var newStock = cartLogic.DecreaseItemQuantityInCart(TestSession, 1, 1);
            Assert.IsTrue(newStock == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]

        public void GetAllCartsForSession_InvalidSession()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.GetAllCartsForSession(new Guid());
        }

        [TestMethod]
        public void GetAllCartsForSession_ValidSession()
        {
            var cartLogic = new CartLogic(1);
            var carts = cartLogic.GetAllCartsForSession(TestSession);
            Assert.IsTrue(carts.Count > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]

        public void AbandonSession_InvalidSession()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.GetAllCartsForSession(new Guid());
        }
        
        [TestMethod]
        public void AbandonSession_ValidSession()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.AbandonSession(TestSession);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void CreateOrderFromCarts_InvalidSession()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.CreateOrderFromCarts(new Guid());
        }

        [TestMethod]
        public void CreateOrderFromCarts_ValidSession()
        {
            var cartLogic = new CartLogic(1);
            cartLogic.CreateOrderFromCarts(TestSession);
        }
    }
}
