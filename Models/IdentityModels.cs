using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Auth.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            
          var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IDBContext
    {
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Registeration> Registeration { get; set; }

        public virtual DbSet<PizzaStock> PizzaStocks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

      

        void IDBContext.MarkAsModified(Pizza pizza)
        {
            Entry(pizza).State = EntityState.Modified;
        }

        void IDBContext.MarkAsModified(Size size)
        {
            Entry(size).State = EntityState.Modified;
        }

        void IDBContext.MarkAsModified(Cart cart)
        {
            Entry(cart).State = EntityState.Modified;
        }

        void IDBContext.MarkAsModified(Order order)
        {
            Entry(order).State = EntityState.Modified;
        }

        void IDBContext.MarkAsModified(OrderDetail orderDetail)
        {
            Entry(orderDetail).State = EntityState.Modified;
        }
        void IDBContext.MarkAsModified(PizzaStock pizzaStock)
        {
            Entry(pizzaStock).State = EntityState.Modified;
        }




        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
    }
}