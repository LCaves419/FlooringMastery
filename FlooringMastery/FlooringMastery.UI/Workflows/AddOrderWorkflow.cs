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

       //private Order _currentOrder;
        private string formattedDate;

        public void Execute()
        {
            CreateDate();
            OpenOrder();
        }


        public void CreateDate()
        {
           
            DateTime currentDate = DateTime.Now;
            OrderOperations ops = new OrderOperations();

            formattedDate = ops.CheckFileDate(currentDate);
        }

        //calls CreateOrder and stores a Order in response.
        public void OpenOrder()
        {
            OrderOperations ops = new OrderOperations();
            var response = ops.CreateOrder(formattedDate);

            Console.WriteLine("Account Information");
            Console.WriteLine("----------------------");
            Console.WriteLine("Account Number {0}", response.OrderInfo.OrderNumber);
            Console.WriteLine("Name: {0}", response.OrderInfo.LastName);
            Console.WriteLine("State: {0}",response.OrderInfo.State);
            Console.WriteLine("TaxRate: {0}",response.OrderInfo.TaxRate);
            Console.WriteLine("ProductType: {0}",response.OrderInfo.ProductType);
            Console.WriteLine("Area: {0}",response.OrderInfo.Area);
            Console.WriteLine("Cost Per Square Foot: {0}",response.OrderInfo.CostSqFt);
            Console.WriteLine("Labor Per Square Foot: {0}", response.OrderInfo.LaborSqFt);
            Console.WriteLine("Material Cost: {0}", response.OrderInfo.MaterialCost);
            Console.WriteLine("Labor Cost: {0}", response.OrderInfo.LaborCost);
            Console.WriteLine("Tax: {0}", response.OrderInfo.Tax);
            Console.WriteLine("Total: {0:c}",response.OrderInfo.Total);
            Console.WriteLine();
        }

    }
}

