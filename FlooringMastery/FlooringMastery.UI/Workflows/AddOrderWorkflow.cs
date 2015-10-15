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
            Order order = new Order();

            Console.WriteLine("Account Information");
            Console.WriteLine("----------------------");
           
            Console.Write("Enter Last Name: ");
            order.LastName = Console.ReadLine();
            Console.Write("State: ");
            order.State = Console.ReadLine();
            Console.Write("TaxRate: ");
            var input = Console.ReadLine();
            order.TaxRate = System.Convert.ToDecimal(input);     

            Console.Write("ProductType: ");
            order.ProductType = Console.ReadLine();

            Console.Write("Area: ");
            var input1 = Console.ReadLine();
            order.Area = System.Convert.ToDecimal(input1);

            Console.Write("Cost Per Square Foot: ");
            var input2 = Console.ReadLine();
            order.CostSqFt = System.Convert.ToDecimal(input2);

            Console.Write("Labor Per Square Foot: ");
            var input3 = Console.ReadLine();
            order.LaborSqFt = System.Convert.ToDecimal(input3);

            Console.Write("Material Cost: ");
            var input4 = Console.ReadLine();
            order.MaterialCost = System.Convert.ToDecimal(input4);

            Console.Write("Labor Cost: "); 
            var input5 = Console.ReadLine();
            order.LaborCost = System.Convert.ToDecimal(input5);

            Console.Write("Tax: ");
            var input6 = Console.ReadLine();
            order.Tax = System.Convert.ToDecimal(input6);

            Console.Write("Total: ");  
            var input7 = Console.ReadLine();
            order.Total = System.Convert.ToDecimal(input7);
            Console.WriteLine();

            ops.CreateOrder(order, formattedDate);

           Console.Clear();
           Console.WriteLine("Here is your new order information:  ");
            DisplayOrderWorkflow dowf = new DisplayOrderWorkflow();
            dowf.PrintOrderInformation(order);

            Console.WriteLine("Press enter for Main Menu...");
        }



    }
}

