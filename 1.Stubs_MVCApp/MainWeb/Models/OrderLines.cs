namespace OrdersWeb.Models
{
    public class OrderLines
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string ProductName { get; set; }

        public double UnitCost { get; set; }

        public bool IsTaxable { get; set; }

        public int Quantity { get; set; }
    }
}