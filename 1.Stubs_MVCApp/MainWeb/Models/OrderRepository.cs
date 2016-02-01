namespace OrdersWeb.Models
{
    using System;
    using System.Linq;

    public class OrderRepository : IOrderRepository
    {
        public IQueryable<OrderLines> OrderLines(int id)
        {
            using (OrderDbContext db = new OrderDbContext())
            {
                var orderLines = db.OrdersLines.Where(x => x.OrderId == id);
                return orderLines;
            }
        }

        public Order Find(int id)
        {
            using (OrderDbContext db = new OrderDbContext())
            {
                var order = db.Orders.First(x => x.Id == id);
                return order;
            }
        }
    }
}
