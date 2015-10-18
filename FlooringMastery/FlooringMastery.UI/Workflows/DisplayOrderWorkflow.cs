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

/// <summary>
/// validates that they entered an actual date
/// </summary>
/// <returns></returns>
        public string GetOrderDateFromUser()
        {
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n\tEnter an order date (mm/dd/yyyy):  ");
                string input = Console.ReadLine();

                DateTime OrderDate;
                if (DateTime.TryParse(input, out OrderDate))
                {
                   OrderOperations ops = new OrderOperations();
                   var formattedOrderDate= ops.GetOrderDate(OrderDate);
                    if (formattedOrderDate == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("That was not a valid entry....");
                        Console.WriteLine("Press enter to continue...");
                        //TODO: enter error to error log
                        Console.ReadLine();
                    }
                    return  formattedOrderDate;
                }
               
            } while (true);
        }
        /// <summary>
        /// checks to make sure they entered a number
        /// </summary>
        /// <returns></returns>
        public int GetOrderNumberFromUser()
        {
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n\tEnter an order number: ");
                string input = Console.ReadLine();

                int OrderNumber;

                if (int.TryParse(input, out OrderNumber))
                {
                    return OrderNumber;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That was not a valid entry....");
                Console.WriteLine("Press enter to continue....");
                Console.ReadLine();


            } while (true);
        }

        public Order DisplayOrderInformation(string file, int orderNumber)
        {
            var ops = new OrderOperations();
            //string formattedDate = GetOrderDateFromUser();
            //int num = GetOrderNumberFromUser();
            var response = ops.GetOrder(file, orderNumber);
            //_currentOrder = response.OrderInfo;

            if (response.Success)
            {
                _currentOrder = response.OrderInfo;
                PrintOrderInformation(response.OrderInfo);
                return _currentOrder;

                }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred!!");
                Console.WriteLine(response.Message);
                Console.WriteLine("Press enter to continue...");
                return null;
            }
        }

        public void PrintOrderInformation(Order orderInfo)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t**********************");
            Console.WriteLine("\t  Order Number {0}", orderInfo.OrderNumber);
            Console.WriteLine("\t**********************");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t  Name: {0}", orderInfo.LastName);
            Console.WriteLine("\t  State: {0}", orderInfo.State);
            Console.WriteLine("\t  TaxRate: {0}", orderInfo.TaxRate);
            Console.WriteLine("\t  ProductType: {0}", orderInfo.ProductType);
            Console.WriteLine("\t  Area: {0}", orderInfo.Area);
            Console.WriteLine("\t  Cost Per Square Foot: {0}", orderInfo.CostSqFt);
            Console.WriteLine("\t  Labor Per Square Foot: {0}", orderInfo.LaborSqFt);
            Console.WriteLine("\t  Material Cost: {0}", orderInfo.MaterialCost);
            Console.WriteLine("\t  Labor Cost: {0}", orderInfo.LaborCost);
            Console.WriteLine("\t  Tax: {0}", orderInfo.Tax);
            Console.WriteLine("\t  Total: {0:c}", orderInfo.Total);
            Console.WriteLine();
        }

    }
}
