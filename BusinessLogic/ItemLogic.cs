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
        /// <summary>
        /// Given a name, a common item code, a price and a stock, method will create and
        /// return a new item db entity
        /// </summary>
        public Item CreateNewItem(string name, string commonCode, decimal price, int initStock)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
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
        }

        /// <summary>
        /// Returns an item based on a provided ID
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public Item GetItemByID(int id)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var item = context.Items.Find(id);
                if (item == null)
                    throw new RequestErrorException("Item does not exist");
                else
                    return item;
            }
        }

        /// <summary>
        /// Given a common item code shared between related items, will return a list ordered descending by price
        /// </summary>
        /// <exception cref="RequestErrorException">Throws when no items are registered with common item code</exception>
        public List<Item> GetItemsByCommonCode(string commonCode)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var items = context.Items.Where(x => x.CommonCode == commonCode);
                if (items.Count() == 0)
                    throw new RequestErrorException("No items with that common code");
                else
                    return new List<Item>(items.OrderByDescending(x => x.Price));
            }
        }

        /// <summary>
        /// Given an item ID, will return the price listed to the item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public decimal GetPriceForItem(int itemID)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var item = context.Items.Find(itemID);
                if (item == null)
                    throw new RequestErrorException("Item cannot be found");
                else
                    return item.Price;
            }
        }

        /// <summary>
        /// Given an item ID, updates the price for a given item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public void UpdatePriceForItem(int itemID, decimal newPrice)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var item = context.Items.Find(itemID);
                if (item == null)
                    throw new RequestErrorException("Item cannot be found");
                else
                {
                    item.Price = newPrice;
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Given an item ID, will return the stock value of a given item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public int GetStockForItem(int itemID) 
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var item = context.Items.Find(itemID);
                if (item == null)
                    throw new RequestErrorException("Item cannot be found");
                else
                    return item.Stock;
            }
        }


        /// <summary>
        /// Given an item ID, updates the number of stock for a given item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public void ChangeStockForItem(int itemID, int newStock)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var item = context.Items.Find(itemID);
                if (item == null)
                    throw new RequestErrorException("Item cannot be found");
                else
                {
                    item.Stock += newStock;
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Given an item ID, will delete the corresponding item
        /// </summary>
        /// <exception cref="RequestErrorException">Throws if id provided does not map to a database object</exception>
        public void DeleteItem(int itemID)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
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
}
