namespace OrdersWeb.Controllers
{
    using System.Web.Mvc;
    using OrdersWeb.Models;
    using System.Linq;

    /// <summary>
    /// This is an example of a MVC controller where the dependency of the OrderDbContext is abstracted through IOrderRepository
    /// </summary>
    public class OrderControllerRepo : Controller
    {
        IOrderRepository _repo;

        public OrderControllerRepo()
        {
            _repo = new OrderRepository();
        }

        public OrderControllerRepo(IOrderRepository repo)
        {
            _repo = repo;
        }

        public ActionResult OrderLines(int id)
        {
            var order = _repo.Find(id);

            // get the corresponding orderlines
            var orderLines = _repo.OrderLines(id);

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
