using Microsoft.EntityFrameworkCore;
using SmartInventory.Entities;

namespace SmartInventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******** Welcome to Smart Inventory Management System ********");

            using var context = new AppDbcontext();

            // =========================================================
            // Explicit Loading
            // =========================================================

            var customer = context.Customers.Find(1);

            if (customer != null)
            {
                context.Entry(customer)
                    .Collection(c => c.Orders)
                    .Load();

                Console.WriteLine($"Customer Orders Count: {customer.Orders.Count}");
            }

            var order = context.Orders.Find(1);

            if (order != null)
            {
                context.Entry(order)
                    .Reference(o => o.Customer)
                    .Load();

                Console.WriteLine($"Order Customer: {order.Customer.CustomerName}");
            }


            // =========================================================
            // Lazy Loading
            // =========================================================

            var lazyOrder = context.Orders.Find(1);

            if (lazyOrder != null)
            {
                Console.WriteLine($"Customer Name: {lazyOrder.Customer.CustomerName}");
            }


            // =========================================================
            // Eager Loading
            // =========================================================

            var customerWithOrders = context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefault(c => c.CustomerID == 1);


            // =========================================================
            // Basic Queries
            // =========================================================

            var ordersByCustomer = context.Orders
                .Where(o => o.CustomerID == 1)
                .ToList();

            var latestOrders = context.Orders
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            var ordersByCustomerAndDate = context.Orders
                .OrderBy(o => o.CustomerID)
                .ThenByDescending(o => o.OrderDate)
                .ToList();


            // =========================================================
            // Projection
            // =========================================================

            var orderSummary = context.Orders
                .Select(o => new
                {
                    o.OrderID,
                    o.CustomerID,
                    o.OrderDate
                })
                .ToList();


            // =========================================================
            // FirstOrDefault / Single / SingleOrDefault
            // =========================================================

            var firstOrder = context.Orders
                .FirstOrDefault(o => o.CustomerID == 1);

            var singleOrder = context.Orders
                .SingleOrDefault(o => o.OrderID == 5);

            var singleOrDefaultOrder = context.Orders
                .SingleOrDefault(o => o.OrderID == 5);


            // =========================================================
            // Any / Count / Sum / Average
            // =========================================================

            var hasExpensiveOrderItem = context.OrderItems
                .Any(i => i.SubTotal >= 1000);

            var customerOrderCount = context.Orders
                .Count(o => o.CustomerID == 1);

            var totalOrderItemsValue = context.OrderItems
                .Sum(i => i.SubTotal);

            var averageProductPrice = context.Products
                .Average(p => p.ProductPrice);


            // =========================================================
            // Queries With Navigation Properties
            // =========================================================

            var ahmedOrders = context.Orders
                .Where(o => o.Customer.CustomerName == "Ahmed")
                .ToList();

            var customerOrdersByDate = context.Orders
                .Where(o => o.CustomerID == 1)
                .Select(o => new
                {
                    o.OrderID,
                    o.OrderDate
                })
                .OrderByDescending(o => o.OrderDate)
                .ToList();


            // =========================================================
            // OrderItem Queries
            // =========================================================

            var orderItems = context.OrderItems
                .Where(i => i.Order.OrderID == 5 && i.Quantity > 2)
                .ToList();


            // =========================================================
            // Queries With Collections
            // =========================================================

            var customersWithMultipleOrders = context.Customers
                .Where(c => c.Orders.Count > 1)
                .Select(c => new
                {
                    c.CustomerName,
                    OrderCount = c.Orders.Count
                })
                .ToList();


            // =========================================================
            // Aggregation With Related Data
            // =========================================================

            var ordersWithTotal = context.Orders
                .Where(o => o.OrderItems.Sum(i => i.SubTotal) >= 1000)
                .Select(o => new
                {
                    o.OrderID,
                    Total = o.OrderItems.Sum(i => i.SubTotal)
                })
                .ToList();


            // =========================================================
            // Customers With At Least One Order
            // =========================================================

            var customersWithOrders = context.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new
                {
                    c.CustomerName,
                    OrderCount = c.Orders.Count
                })
                .ToList();


            Console.WriteLine("\nAll EF Core demonstrations completed.");
        }
    }
}