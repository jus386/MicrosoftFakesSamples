using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersWeb.Controllers;
using OrdersWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrdersWeb.Controllers.Tests
{
    /// <summary>
    /// Unit test where the test double of IOrderRepository is created as ordinary class
    /// </summary>
    [TestClass()]
    public class OrderControllerTests
    {
        [TestMethod()]
        public void OrderLines_SetOrder_TotalAmount()
        {
            // arrange
            var stubRepo = new StubOrderRepository();
            OrderControllerRepo controller = new OrderControllerRepo(stubRepo);
            var orderId = 1;

            // act 
            var result = controller.OrderLines(orderId) as ViewResult;
            var viewModel = result.Model as OrderSummaryViewModel;

            // assert
            Assert.AreEqual(viewModel.Total, 5675, "Order summary total not correct");
        }
    }

    /// <summary>
    /// Test double of IOrderRepository
    /// </summary>
    public class StubOrderRepository : IOrderRepository
    {
        public Order Find(int id)
        {
            if (id != 1)
            {
                return null;
            }
            return new Order
            {
                Id = 10,
                CustomerName = "Test",
                TaxRate = 5
            };
        }

        public IQueryable<OrderLines> OrderLines(int id)
        {
            var orderLines = new List<OrderLines>
                        {
                            new OrderLines { Id = 1, IsTaxable = true, ProductName = "widget1", Quantity = 10, UnitCost = 10 },
                            new OrderLines { Id = 1, IsTaxable = false, ProductName = "widget2", Quantity = 20, UnitCost = 20 },
                            new OrderLines { Id = 1, IsTaxable = true, ProductName = "widget3", Quantity = 30, UnitCost = 30 },
                            new OrderLines { Id = 1, IsTaxable = false, ProductName = "widget4", Quantity = 40, UnitCost = 40 },
                            new OrderLines { Id = 1, IsTaxable = true, ProductName = "widget5", Quantity = 50, UnitCost = 50 }
                        };
            return orderLines.AsQueryable();
        }
    }
}