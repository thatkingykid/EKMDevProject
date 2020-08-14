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
    public class ItemLogicUnitTests
    {
        [TestMethod]
        public void AddNewItem_Test()
        {
            var itemLogic = new ItemLogic();
            var item = itemLogic.CreateNewItem("Test Item", "TEST", 200, 20);
            Assert.IsTrue(item != null);
        }

        [TestMethod]
        public void GetAllItems_Test()
        {
            var itemLogic = new ItemLogic();
            var items = itemLogic.GetAllItems();
            Assert.IsTrue(items.Count() > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void GetItemByID_InvalidTest()
        {
            var itemLogic = new ItemLogic();
            var item = itemLogic.GetItemByID(10000000);
        }

        [TestMethod]
        public void GetItemByID_ValidTest()
        {
            var itemLogic = new ItemLogic();
            var item = itemLogic.GetItemByID(1);
            Assert.IsTrue(item.Name == "Test Item");
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void GetItemsByCommonCode_InvalidTest()
        {
            var itemLogic = new ItemLogic();
            var items = itemLogic.GetItemsByCommonCode("QWERTYUI");
        }

        [TestMethod]
        public void GetItemsByCommonCode_ValidTest()
        {
            var itemLogic = new ItemLogic();
            var items = itemLogic.GetItemsByCommonCode("TEST");
            Assert.IsTrue(items.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void GetPriceForItem_InvalidItem()
        {
            var itemLogic = new ItemLogic();
            itemLogic.GetPriceForItem(10000000);
        }

        [TestMethod]
        public void GetPriceForItem_ValidItem()
        {
            var itemLogic = new ItemLogic();
            var item = itemLogic.GetPriceForItem(1);
            Assert.IsTrue(item == 200);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void UpdatePriceForItem_InvalidItem()
        {
            var itemLogic = new ItemLogic();
            itemLogic.UpdatePriceForItem(10000000, 4000);
        }

        [TestMethod]
        public void UpdatePriceForItem_ValidItem()
        {
            var itemLogic = new ItemLogic();
            itemLogic.UpdatePriceForItem(1, 2001);
            var item = itemLogic.GetPriceForItem(1);
            itemLogic.UpdatePriceForItem(1, 200);
            Assert.IsTrue(item == 2001);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void GetStockForItem_InvalidItem()
        {
            var itemLogic = new ItemLogic();
            itemLogic.GetStockForItem(10000000);
        }

        [TestMethod]
        public void GetStockForItem_ValidItem()
        {
            var itemLogic = new ItemLogic();
            var stock = itemLogic.GetStockForItem(1);
            Assert.IsTrue(stock == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void AddStockToItem_InvalidItem()
        {
            var itemLogic = new ItemLogic();
            itemLogic.AddStockToItem(10000000, 20);
        }

        [TestMethod]
        public void AddStockToItem_ValidItem()
        {
            var itemLogic = new ItemLogic();
            var newStock = itemLogic.AddStockToItem(1, 1);
            Assert.IsTrue(newStock == 21);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void ReduceItemStock_InvalidItem()
        {
            var itemLogic = new ItemLogic();
            itemLogic.ReduceItemStock(10000000, 20);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void ReduceItemStock_InvalidQty()
        {
            var itemLogic = new ItemLogic();
            itemLogic.ReduceItemStock(1, 25);
        }

        [TestMethod]
        public void ReduceItemStock_Valid()
        {
            var itemLogic = new ItemLogic();
            var newStock = itemLogic.ReduceItemStock(1, 1);
            Assert.IsTrue(newStock == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void DeleteItem_InvalidItem()
        {
            var itemLogic = new ItemLogic();
            itemLogic.DeleteItem(100000);
        }

        [TestMethod]
        public void DeleteItem_ValidItem()
        {
            var itemLogic = new ItemLogic();
            var newItem = itemLogic.CreateNewItem("Test 2", "TEST", 20, 1);
            itemLogic.DeleteItem(newItem.ID);
        }
    }
}
