namespace OrdersWeb.Controllers
{
    using System.Web.Mvc;
    using OrdersWeb.Models;
    using System.Linq;

    /// <summary>
    /// This is an example of a MVC controller that is difficult to test due to dependency of the OrderDbContext
    /// </summary>
    public class OrderController : Controller
    {
        public ActionResult OrderLines(int id)
        {
            using (OrderDbContext db = new OrderDbContext())
            {
                var order = db.Orders.First(x => x.Id == id);
                // get the corresponding orderlines
                var orderLines = db.OrdersLines.Where(x => x.OrderId == id);

                // initialize the calculation values
                double total = 0d;
                double taxRate = order.TaxRate / 100;
                double taxMultiplier = 1 + taxRate;

                // run through the list and calculate total
                foreach (var lineItem in orderLines)
                {
                    if (lineItem.IsTaxable)
                    {
                        total += lineItem.Quantity * lineItem.UnitCost * taxMultiplier;
                    }
                    else
                    {
                        total += lineItem.Quantity * lineItem.UnitCost;
                    }
                }

                // make the view model and set its properties
                var viewModel = new OrderSummaryViewModel();
                viewModel.Order = order;
                viewModel.OrderLines = orderLines.ToList();
                viewModel.Total = total;

                return this.View(viewModel);
            }
        }
    }
}
