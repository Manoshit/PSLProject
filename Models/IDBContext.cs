using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public interface IDBContext : IDisposable
    {
        DbSet<Pizza> Pizzas { get;  }
        DbSet<Size> Sizes { get; }
        DbSet<Registeration> Registeration { get; }
        DbSet<Cart> Carts { get; }

        DbSet<Order> Orders { get; }
        DbSet<OrderDetail> OrderDetails { get; }

        DbSet<PizzaStock> PizzaStocks { get; }
        int SaveChanges();
        void MarkAsModified(Pizza pizza);

        void MarkAsModified(Size size);

        void MarkAsModified(Cart cart);

        void MarkAsModified(Order order);
        void MarkAsModified(OrderDetail orderDetail);

        void MarkAsModified(PizzaStock orderDetail);
    }
}