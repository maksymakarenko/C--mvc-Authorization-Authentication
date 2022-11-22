using System;
using System.Linq;
using System.Threading.Tasks;
using TaskAuthenticationAuthorization.Models;
using TaskAuthenticationAuthorization.Models.Auth;
using TaskAuthenticationAuthorization.Models.Auth.Entities;

namespace TaskAuthenticationAuthorization
{
    public class SampleData
    {
        public static void SeedDb(ShoppingContext context)
        {
            if (context.Products.Any()) return;

            context.Products.AddRange(
                new Product { Name = "Butter", Price = 30.0 },
                new Product { Name = "Banana", Price = 20.50 },
                new Product { Name = "Cola", Price = 9.30 }
            );
            context.SaveChanges();         

            var roles = new []
            {
                new Role { Name = "buyer" },
                new Role { Name = "admin" },
            };
           context.Roles.AddRange(roles);
           context.SaveChanges();
            
            var customers = new []
            {
                new Customer
                {
                    FirstName = "Ostap",
                    LastName = "Bender",
                    Address = "Rio de Zhmerinka",
                    Discount = Discount.O,
                },
                new Customer
                {
                    FirstName = "Shura",
                    LastName = "Balaganov",
                    Address = "Odessa",
                    Discount = Discount.R,
                }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
            
            context.Users.AddRange(
                new User
                {
                    Email = "buyer12345@gmail.com",
                    Password = "qwerty123",
                    Role = roles[0],
                    RoleId = roles[0].Id,
                    Customer = customers[0],
                },
                new User
                {
                    Email = "admin12345@gmail.com",
                    Password = "admin12345",
                    Role = roles[1],
                    RoleId = roles[1].Id,
                    Customer = customers[1],
                }
            );
            context.SaveChanges();

            context.SuperMarkets.AddRange(
                new SuperMarket { Name = "Wellmart", Address = "Lviv", },
                new SuperMarket { Name = "Billa", Address = "Odessa", }
            );
            context.SaveChanges();

            context.Orders.AddRange(
                new Order
                {
                    CustomerId = 2,
                    SuperMarketId = 1,
                    OrderDate = DateTime.Now,
                },
                new Order
                {
                    CustomerId = 2,
                    SuperMarketId = 1,
                    OrderDate = DateTime.Now,
                }
            );
            context.SaveChanges();

            context.OrderDetails.AddRange(
              new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 2

                },
                new OrderDetail
                {
                    OrderId = 2,
                    ProductId = 2,
                    Quantity = 1
                }
            );
            context.SaveChanges();
        }
    }
}
