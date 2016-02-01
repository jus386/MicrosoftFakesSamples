namespace OrdersWeb.Models
{
    using System.Collections.Generic;

    public class OrderSummaryViewModel
    {
        public Order Order { get; set; }

        public List<OrderLines> OrderLines { get; set; }

        public double Total { get; set; }
    }
}