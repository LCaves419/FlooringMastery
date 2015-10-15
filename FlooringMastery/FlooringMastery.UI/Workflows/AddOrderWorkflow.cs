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
            formattedDate = CreateDate();
            PopulateOrder(formattedDate);
        }


        public string CreateDate()
        {
           
            DateTime currentDate = DateTime.Now;
            OrderOperations ops = new OrderOperations();

            formattedDate = ops.CheckFileDate(currentDate);
            return formattedDate;
        }

        //calls CreateOrder and stores a Order in response.
        public void PopulateOrder(string formattedDate)
        {
            OrderOperations ops = new OrderOperations();
            var response = new Response();

            Console.WriteLine("Account Information");
            Console.WriteLine("----------------------");
           
            Console.WriteLine("Enter Last Name: ");
            response.OrderInfo.LastName = Console.ReadLine();
            Console.WriteLine("State: ");
            response.OrderInfo.State = Console.ReadLine();
            Console.WriteLine("TaxRate: ");
            var input = Console.ReadLine();
            response.OrderInfo.TaxRate = System.Convert.ToDecimal(input);     

            Console.WriteLine("ProductType: ");
            response.OrderInfo.ProductType = Console.ReadLine();

            Console.WriteLine("Area: ");
            var input1 = Console.ReadLine();
            response.OrderInfo.Area = System.Convert.ToDecimal(input);

            Console.WriteLine("Cost Per Square Foot: ");
            var input2 = Console.ReadLine();
            response.OrderInfo.CostSqFt = System.Convert.ToDecimal(input);

            Console.WriteLine("Labor Per Square Foot: ");
            var input3 = Console.ReadLine();
            response.OrderInfo.LaborSqFt = System.Convert.ToDecimal(input);

            Console.WriteLine("Material Cost: ");
            var input4 = Console.ReadLine();
            response.OrderInfo.MaterialCost = System.Convert.ToDecimal(input);

            Console.WriteLine("Labor Cost: "); 
            var input5 = Console.ReadLine();
            response.OrderInfo.LaborCost = System.Convert.ToDecimal(input);

            Console.WriteLine("Tax: ");
            var input6 = Console.ReadLine();
            response.OrderInfo.Tax = System.Convert.ToDecimal(input);

            Console.WriteLine("Total: ");  
            var input7 = Console.ReadLine();
            response.OrderInfo.Total = System.Convert.ToDecimal(input);
            Console.WriteLine();

            ops.CreateOrder(response.OrderInfo, formattedDate);
            
        }

    }
}

