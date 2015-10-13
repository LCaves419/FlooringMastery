using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class DisplayOrderWorkflow
    {
        private Order _currentOrder;

        public void Execute()
        {
            DateTime OrderDate = GetOrderDateFromUser();
            int OrderNumber = GetOrderNumberFromUser();
            //DisplayOrderInformation(OrderNumber);

        }


        public DateTime GetOrderDateFromUser()
        {
            do
            {
                Console.Clear();
                Console.Write("enter an order date (mm/dd/yyyy):  ");
                string input = Console.ReadLine();

                DateTime OrderDate;
                if (DateTime.TryParse(input, out OrderDate))
                {
                    return OrderDate;
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

        //public void DisplayOrderInformation(int orderNumber)
        //{
        //    var ops = new OrderOperations();
        //    var response = ops.GetAccount(accountNumber);

        //    if (response.Success)
        //    {
        //        _currentAccount = response.AccountInfo;
        //        PrintAccountInformation(response.AccountInfo);

        //        DisplayAccountMenu();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Error occurred!!");
        //        Console.WriteLine(response.Message);
        //        Console.WriteLine("Press enter to continue...");
        //        Console.ReadLine();
        //    }
        //}

        //public void PrintAccountInformation(Account AccountInfo)
        //{
        //    Console.WriteLine("Account Information");
        //    Console.WriteLine("----------------------");
        //    Console.WriteLine("Account Number {0}", AccountInfo.AccountNumber);
        //    Console.WriteLine("Name: {0}, {1}", AccountInfo.LastName, AccountInfo.FirstName);
        //    Console.WriteLine("Account Balance: {0:c}", AccountInfo.Balance);
        //    Console.WriteLine();

        //}

    }
}
