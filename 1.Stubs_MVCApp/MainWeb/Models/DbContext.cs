using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrdersWeb.Models
{
    public class OrderDbContext : DbContext
    {  
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLines> OrdersLines { get; set; }
    }
}