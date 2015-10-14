using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class AddOrderWorkflow
    {

        private Order _currentOrder;
        private string formattedDate;

        public void Execute()
        {
            OpenOrder();
        }


        public void CreateDate()
        {
            DateTime parseDate;
            DateTime currentDate = DateTime.Now;
            OrderOperations ops = new OrderOperations();

            formattedDate = ops.CheckFileDate(currentDate);

        }

        public void OpenOrder()
        {

            OrderOperations ops = new OrderOperations();
            var response = ops.CreateOrder(formattedDate);

            Console.WriteLine("Account Information");
            Console.WriteLine("----------------------");
            Console.WriteLine("Account Number {0}", OrderInfo.OrderNumber);
            Console.WriteLine("Name: {0}", OrderInfo.LastName);
            Console.WriteLine("State: {0}", OrderInfo.State);
            Console.WriteLine("TaxRate: {0}", OrderInfo.TaxRate);
            Console.WriteLine("ProductType: {0}", OrderInfo.ProductType);
            Console.WriteLine("Area: {0}", OrderInfo.Area);
            Console.WriteLine("Cost Per Square Foot: {0}", OrderInfo.CostSqFt);
            Console.WriteLine("Labor Per Square Foot: {0}", OrderInfo.LaborSqFt);
            Console.WriteLine("Material Cost: {0}", OrderInfo.MaterialCost);
            Console.WriteLine("Labor Cost: {0}", OrderInfo.LaborCost);
            Console.WriteLine("Tax: {0}", OrderInfo.Tax);
            Console.WriteLine("Total: {0:c}", OrderInfo.Total);
            Console.WriteLine();
        }

    }
}

