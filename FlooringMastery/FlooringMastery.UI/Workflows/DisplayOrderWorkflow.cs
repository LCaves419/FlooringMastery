using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Data;

namespace FlooringMastery.UI.Workflows
{
    public class DisplayOrderWorkflow
    {
        private Order _currentOrder;
        private string file;
        private int OrderNumber;

        public void Execute()
        {
           // Order newOrder = new Order();
            file = GetOrderDateFromUser();
            OrderNumber = GetOrderNumberFromUser();
            DisplayOrderInformation(file, OrderNumber);
        }


        public string GetOrderDateFromUser()
        {
            do
            {
                Console.Clear();
                Console.Write("enter an order date (mm/dd/yyyy):  ");
                string input = Console.ReadLine();

                DateTime OrderDate;
                if (DateTime.TryParse(input, out OrderDate))
                {
                   OrderOperations ops = new OrderOperations();
                   var formattedOrderDate= ops.GetOrderDate(OrderDate);
                    return  formattedOrderDate;
                }

                Console.WriteLine("That was not a valid order date....");
                Console.WriteLine("Press enter to continue...");
                //TODO: enter error to error log
                Console.ReadLine();

            } while (true);
        }

        public int GetOrderNumberFromUser()
        {
            do
            {
                Console.Clear();
                Console.Write("Enter an order number: ");
                string input = Console.ReadLine();

                int OrderNumber;
                if (int.TryParse(input, out OrderNumber))
                {
                    return OrderNumber;
                }

                Console.WriteLine("That was not a valid order number....");
                Console.WriteLine("Press enter to continue....");
                Console.ReadLine();


            } while (true);
        }

        public void DisplayOrderInformation(string file, int orderNumber)
        {
            var ops = new OrderOperations();
            //string formattedDate = GetOrderDateFromUser();
            //int num = GetOrderNumberFromUser();
            var response = ops.GetOrder(file, orderNumber);
            _currentOrder = response.OrderInfo;

            if (response.Success)
            {
                //_currentOrder = response.OrderInfo;
                PrintOrderInformation(response.OrderInfo);

                }
            else
            {
                Console.WriteLine("Error occurred!!");
                Console.WriteLine(response.Message);
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
        }

        public void PrintOrderInformation(Order OrderInfo)
        {
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
