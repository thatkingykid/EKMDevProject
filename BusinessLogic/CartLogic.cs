using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CartLogic
    {
        private int userID;
        public CartLogic(int user)
        {
            userID = user;
        }

        public Guid CreateNewCartSession()
        {
            return new Guid();
        }

        public Cart AddNewCartItem(Guid cartSession, int itemID, int qty)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var itemExits = context.Items.Find(itemID) != null;
                if (itemExits)
                    throw new RequestErrorException("Item chosen is invalid");

                var cart = new Cart
                {
                    SessionID = cartSession,
                    ItemID = itemID,
                    UserID = userID,
                    Quantity = qty
                };

                context.Carts.Add(cart);
                context.SaveChanges();
                return cart;
            }
        }

        public int RemoveItem(Guid cartSession, int itemID)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var item = context.Carts.FirstOrDefault(x => x.SessionID == cartSession && x.ItemID == itemID);
                if (item == null)
                    throw new RequestErrorException("Selected item is not in cart");

                var stockToRetain = item.Quantity;
                context.Carts.Remove(item);
                context.SaveChanges();
                return stockToRetain;
            }
        }

        public int IncreaseItemQuantityInCart(Guid cartSession, int itemID, int quantityToAdd)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var item = context.Carts.FirstOrDefault(x => x.SessionID == cartSession && x.ItemID == itemID);
                if (item == null)
                    throw new RequestErrorException("Selected item is not in cart");

                item.Quantity += quantityToAdd;
                context.SaveChanges();
                return item.Quantity;
            }
        }

        public int DecreaseItemQuantityInCart(Guid cartSession, int itemID, int quantityToRemove)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var item = context.Carts.FirstOrDefault(x => x.SessionID == cartSession && x.ItemID == itemID);
                if (item == null)
                    throw new RequestErrorException("Selected item is not in cart");

                item.Quantity += quantityToRemove;
                context.SaveChanges();
                return item.Quantity;
            }
        }

        public List<Cart> GetAllCartsForSession(Guid session)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var carts = context.Carts.Where(x => x.SessionID == session);
                if (carts.Count() == 0)
                    return null;
                else return new List<Cart>(carts);
            }
        }

        public void AbandonSession(Guid session)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var carts = context.Carts.Where(x => x.SessionID == session);
                if (carts.Count() == 0)
                    return;
                else
                    context.Carts.RemoveRange(carts);
            }
        }

        public Order CreateOrderFromCarts(Guid sessionID)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var carts = context.Carts.Where(x => x.SessionID == sessionID);
                if (carts.Count() == 0)
                    throw new RequestErrorException("Cart must have a single item to turn into an order");
                else
                {
                    decimal totalVal = 0;
                    foreach (var item in carts)
                    {
                        totalVal += item.Quantity * item.Item.Price;
                    }

                    var order = new Order
                    {
                        CartID = sessionID,
                        UserID = userID,
                        TotalValue = totalVal
                    };
                    context.Orders.Add(order);
                    context.SaveChanges();
                    return order;
                }
            }
        }
    }
}
