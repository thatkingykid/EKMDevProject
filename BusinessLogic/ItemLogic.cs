using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    public class ItemLogic
    {
        private EKMDemoEntities context = null;
        public ItemLogic(EKMDemoEntities contextObj = null)
        {
                context = contextObj;
        }

        /// <summary>
        /// Given a name, a common item code, a price and a stock, method will create and
        /// return a new item db entity
        /// </summary>
        public Item CreateNewItem(string name, string commonCode, decimal price, int initStock)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var newItem = new Item
            {
                Name = name,
                Stock = initStock,
                CommonCode = commonCode,
                Price = price
            };
            context.Items.Add(newItem);
            context.SaveChanges();
            return newItem;

        }

        public List<Item> GetAllItems()
        {
            if (context == null)
                context = new EKMDemoEntities();

            return new List<Item>(context.Items);

        }

        /// <summary>
        /// Returns an item based on a provided ID
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public Item GetItemByID(int id)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var item = context.Items.Find(id);
            if (item == null)
                throw new RequestErrorException("Item does not exist");
            else
                return item;

        }

        /// <summary>
        /// Given a common item code shared between related items, will return a list ordered descending by price
        /// </summary>
        /// <exception cref="RequestErrorException">Throws when no items are registered with common item code</exception>
        public List<Item> GetItemsByCommonCode(string commonCode)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var items = context.Items.Where(x => x.CommonCode == commonCode);
            if (items.Count() == 0)
                throw new RequestErrorException("No items with that common code");
            else
                return new List<Item>(items.OrderByDescending(x => x.Price));

        }

        /// <summary>
        /// Given an item ID, will return the price listed to the item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public decimal GetPriceForItem(int itemID)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var item = context.Items.Find(itemID);
            if (item == null)
                throw new RequestErrorException("Item cannot be found");
            else
                return item.Price;
        }

        /// <summary>
        /// Given an item ID, updates the price for a given item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public void UpdatePriceForItem(int itemID, decimal newPrice)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var item = context.Items.Find(itemID);
            if (item == null)
                throw new RequestErrorException("Item cannot be found");
            else
            {
                item.Price = newPrice;
                context.SaveChanges();
            }

        }

        /// <summary>
        /// Given an item ID, will return the stock value of a given item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public int GetStockForItem(int itemID)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var item = context.Items.Find(itemID);
            if (item == null)
                throw new RequestErrorException("Item cannot be found");
            else
                return item.Stock;

        }


        /// <summary>
        /// Given an item ID, updates the number of stock for a given item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public int AddStockToItem(int itemID, int stockToAdd)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var item = context.Items.Find(itemID);
            if (item == null)
                throw new RequestErrorException("Item cannot be found");
            else
            {
                item.Stock += stockToAdd;
                context.SaveChanges();
                return item.Stock;
            }

        }

        public int ReduceItemStock(int itemID, int stockToRemove)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var item = context.Items.Find(itemID);
            if (item == null)
                throw new RequestErrorException("Item cannot be found");
            else
            {
                if (item.Stock - stockToRemove < 0)
                    throw new RequestErrorException("Cannot reduce stock to negative quantity");
                item.Stock -= stockToRemove;
                context.SaveChanges();
                return item.Stock;
            }

        }

        /// <summary>
        /// Given an item ID, will delete the corresponding item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public void DeleteItem(int itemID)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var item = context.Items.Find(itemID);
            if (item == null)
                throw new RequestErrorException("Item cannot be found");
            else
            {
                context.Items.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
