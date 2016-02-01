namespace OrdersWeb.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public double TaxRate { get; set; }
    }
}