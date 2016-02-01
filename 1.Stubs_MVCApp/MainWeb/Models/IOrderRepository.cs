namespace OrdersWeb.Models
{
    using System.Linq;

    public interface IOrderRepository
    {
        IQueryable<OrderLines> OrderLines(int id);

        Order Find(int id);
    }
}
