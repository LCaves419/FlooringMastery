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
        ErrorLog log = new ErrorLog();
        OrderOperations ops = new OrderOperations();
        private Order _currentOrder;
        private string file;
        private int OrderNumber;

        public void Execute()
        {
           // Order newOrder = new Order();
            file = GetOrderDateFromUser();
            OrderNumber = DisplayAllAccounts(file);
            DisplayOrderInformation(file, OrderNumber);
        }

        public int DisplayAllAccounts(string file)
        {
            Console.Clear();
            var ops = new OrderOperations();
            //get all orders from the entered file
            List<Order> allOrdersToDisplay = ops.GetAllOrders(file);
            Console.WriteLine("\t");
            foreach (var a in allOrdersToDisplay)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t************************");
                Console.WriteLine("\tOrder Number: {0}", a.OrderNumber);
                Console.WriteLine("\t************************");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\tName: {0}", a.LastName);
                Console.WriteLine("\tState: {0}", a.State);
            }
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n  PLEASE CH0OSE AN ORDER NUMBER....");
                string input = Console.ReadLine();

                int OrderNumber;

                if (int.TryParse(input, out OrderNumber))
                {
                    Console.Clear();
                    return OrderNumber;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tThat was not a valid entry....");
                log.ErrorMessage = "That was not a valid order number entry UI:DisplayALlAccounts....";
                ops.CallingErrorLogRepository(log.ErrorMessage);
                Console.WriteLine("\tPress enter to continue....");
                Console.ReadLine();


            } while (true);

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

                    if (input.Length < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\tThat was not a valid entry....");
                        Console.WriteLine("\tPress enter to continue...");
                        log.ErrorMessage = "That was not a valid entry UI:getOrderDateFromUser....";
                        ops.CallingErrorLogRepository(log.ErrorMessage);
                        //TODO: enter error to error log
                        Console.ReadLine();
                    }
                    else
                    {
                        DateTime OrderDate;
                        if (DateTime.TryParse(input, out OrderDate))
                        {
                           
                            var formattedOrderDate = ops.GetOrderDate(OrderDate);
                            if (formattedOrderDate == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\tThat was not a valid date....");
                                Console.WriteLine("\tPress enter to continue...");
                            log.ErrorMessage = "That was not a valid date UI:getOrderDateFromUser....";
                            ops.CallingErrorLogRepository(log.ErrorMessage);
                            Console.ReadLine();
                            }
                            return formattedOrderDate;
                        }
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
                Console.WriteLine("\tThat was not a valid entry....");
                Console.WriteLine("\tPress enter to continue....");
                log.ErrorMessage = "That was not a valid order number UI:getOrderNumFromUser....";
                ops.CallingErrorLogRepository(log.ErrorMessage);
                Console.ReadLine();
                DisplayOrderInformation(file, OrderNumber);


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
                    Console.WriteLine("\tError occurred!!");
                    Console.WriteLine(response.Message);
                    Console.WriteLine("\tPress enter to continue...");
                log.ErrorMessage = "That was not a valid order UI:DisplayOrdererInfo....";
                ops.CallingErrorLogRepository(log.ErrorMessage);
                Console.ReadLine();
                    GetOrderDateFromUser();
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

            Console.WriteLine("\t  Name: {0}", orderInfo.LastName.ToUpper());
            Console.WriteLine("\t  State: {0}", orderInfo.State.ToUpper());
            Console.WriteLine("\t  TaxRate: {0}", orderInfo.TaxRate);
            Console.WriteLine("\t  ProductType: {0}", orderInfo.ProductType.ToUpper());
            Console.WriteLine("\t  Area: {0}", orderInfo.Area);
            Console.WriteLine("\t  Cost Per Square Foot: {0:c}", orderInfo.CostSqFt);
            Console.WriteLine("\t  Labor Per Square Foot: {0:c}", orderInfo.LaborSqFt);
            Console.WriteLine("\t  Material Cost: {0:c}", orderInfo.MaterialCost);
            Console.WriteLine("\t  Labor Cost: {0:c}", orderInfo.LaborCost);
            Console.WriteLine("\t  Tax: {0:c}", orderInfo.Tax);
            Console.WriteLine("\t  Total: {0:c}", orderInfo.Total);
            Console.WriteLine();
            Console.WriteLine("\tPress Enter for Main Menu");
        }

    }
}
