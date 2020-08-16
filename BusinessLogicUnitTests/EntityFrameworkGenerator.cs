using DataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicUnitTests
{
    public static class EntityFrameworkGenerator
    {
        public static EKMDemoEntities GenerateMockFramework(List<SysUser> users = null, List<Auth> auths = null, List<Item> items = null, List<Cart> carts = null, List<Order> orders = null)
        {
            if (users == null)
                users = new List<SysUser>();
            var userData = users.AsQueryable();

            var userMock = new Mock<DbSet<SysUser>>();
            userMock.As<IQueryable<SysUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            userMock.As<IQueryable<SysUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            userMock.As<IQueryable<SysUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            userMock.As<IQueryable<SysUser>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            userMock.Setup(x => x.Add(It.IsAny<SysUser>())).Returns<SysUser>(arg => arg).Callback<SysUser>((s) => userData.Concat(new[] { s }));
            userMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => userData.FirstOrDefault(d => d.ID == (int)ids[0]));

            if (auths == null)
                auths = new List<Auth>();
            var authData = auths.AsQueryable();

            var authMock = new Mock<DbSet<Auth>>();
            authMock.As<IQueryable<Auth>>().Setup(m => m.Provider).Returns(authData.Provider);
            authMock.As<IQueryable<Auth>>().Setup(m => m.Expression).Returns(authData.Expression);
            authMock.As<IQueryable<Auth>>().Setup(m => m.ElementType).Returns(authData.ElementType);
            authMock.As<IQueryable<Auth>>().Setup(m => m.GetEnumerator()).Returns(() => authData.GetEnumerator());
            authMock.Setup(x => x.Add(It.IsAny<Auth>())).Returns<Auth>(arg => arg).Callback<Auth>((s) => authData.Concat(new[] { s }));
            authMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => authData.FirstOrDefault(d => d.ID == (int)ids[0]));


            if (items == null)
                items = new List<Item>();
            var itemData = items.AsQueryable();
            var itemMock = new Mock<DbSet<Item>>();
            itemMock.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(itemData.Provider);
            itemMock.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(itemData.Expression);
            itemMock.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(itemData.ElementType);
            itemMock.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(() => itemData.GetEnumerator());
            itemMock.Setup(x => x.Add(It.IsAny<Item>())).Returns<Item>(arg => arg).Callback<Item>((s) => itemData.Concat(new[] { s }));
            itemMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => itemData.FirstOrDefault(d => d.ID == (int)ids[0]));


            if (carts == null)
                carts = new List<Cart>();
            var cartData = carts.AsQueryable();
            var cartMock = new Mock<DbSet<Cart>>();
            cartMock.As<IQueryable<Cart>>().Setup(m => m.Provider).Returns(cartData.Provider);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.Expression).Returns(cartData.Expression);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.ElementType).Returns(cartData.ElementType);
            cartMock.As<IQueryable<Cart>>().Setup(m => m.GetEnumerator()).Returns(() => cartData.GetEnumerator());
            cartMock.Setup(x => x.Add(It.IsAny<Cart>())).Returns<Cart>(arg => arg).Callback<Cart>((s) => cartData.Concat(new[] { s }));
            cartMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => cartData.FirstOrDefault(d => d.ID == (int)ids[0]));


            if (orders == null)
                orders = new List<Order>();
            var orderData = orders.AsQueryable();
            var orderMock = new Mock<DbSet<Order>>();
            orderMock.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orderData.Provider);
            orderMock.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orderData.Expression);
            orderMock.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orderData.ElementType);
            orderMock.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(() => orderData.GetEnumerator());
            orderMock.Setup(x => x.Add(It.IsAny<Order>())).Returns<Order>(arg => arg).Callback<Order>((s) => orderData.Concat(new[] { s }));
            orderMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => orderData.FirstOrDefault(d => d.ID == (int)ids[0]));

            var mockContext = new Mock<EKMDemoEntities>();
            mockContext.Setup(x => x.Auths).Returns(authMock.Object);
            mockContext.Setup(x => x.Carts).Returns(cartMock.Object);
            mockContext.Setup(x => x.Orders).Returns(orderMock.Object);
            mockContext.Setup(x => x.Items).Returns(itemMock.Object);
            mockContext.Setup(x => x.SysUsers).Returns(userMock.Object);
            return mockContext.Object;
        }
    }
}
