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
    public class ItemLogicUnitTests
    {
        [TestMethod]
        public void AddNewItem_Test()
        {
            var efContext = EntityFrameworkGenerator.GenerateMockFramework();
            var itemLogic = new ItemLogic(efContext);
            var item = itemLogic.CreateNewItem("Test Item", "TEST", 200, 20);
            Assert.IsTrue(item != null);
        }

        [TestMethod]
        public void GetAllItems_Test()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            var itemResults = itemLogic.GetAllItems();
            Assert.IsTrue(itemResults.Count() > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void GetItemByID_InvalidTest()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            var item = itemLogic.GetItemByID(10000000);
        }

        [TestMethod]
        public void GetItemByID_ValidTest()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "Test Item", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            var item = itemLogic.GetItemByID(1);
            Assert.IsTrue(item.Name == "Test Item");
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void GetItemsByCommonCode_InvalidTest()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.GetItemsByCommonCode("QWERTYUI");
        }

        [TestMethod]
        public void GetItemsByCommonCode_ValidTest()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            var itemResult = itemLogic.GetItemsByCommonCode("TEST");
            Assert.IsTrue(itemResult.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void GetPriceForItem_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.GetPriceForItem(10000000);
        }

        [TestMethod]
        public void GetPriceForItem_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 200, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            var item = itemLogic.GetPriceForItem(1);
            Assert.IsTrue(item == 200);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void UpdatePriceForItem_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.UpdatePriceForItem(10000000, 4000);
        }

        [TestMethod]
        public void UpdatePriceForItem_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.UpdatePriceForItem(1, 2001);
            var item = itemLogic.GetPriceForItem(1);
            Assert.IsTrue(item == 2001);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void GetStockForItem_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.GetStockForItem(10000000);
        }

        [TestMethod]
        public void GetStockForItem_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock = 20 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            var stock = itemLogic.GetStockForItem(1);
            Assert.IsTrue(stock == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void AddStockToItem_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.AddStockToItem(10000000, 20);
        }

        [TestMethod]
        public void AddStockToItem_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock = 20 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            var newStock = itemLogic.AddStockToItem(1, 1);
            Assert.IsTrue(newStock == 21);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void ReduceItemStock_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.ReduceItemStock(10000000, 20);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void ReduceItemStock_InvalidQty()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.ReduceItemStock(1, 25);
        }

        [TestMethod]
        public void ReduceItemStock_Valid()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =21 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            var newStock = itemLogic.ReduceItemStock(1, 1);
            Assert.IsTrue(newStock == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestErrorException))]
        public void DeleteItem_InvalidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.DeleteItem(100000);
        }

        [TestMethod]
        public void DeleteItem_ValidItem()
        {
            var items = new List<Item>
            {
                new Item{ID = 1, Name = "test", CommonCode = "TEST", Price = 0, Stock =0 }
            };
            var efContext = EntityFrameworkGenerator.GenerateMockFramework(items: items);
            var itemLogic = new ItemLogic(efContext);
            itemLogic.DeleteItem(1);
        }
    }
}
