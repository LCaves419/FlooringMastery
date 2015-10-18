using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.BLL;

namespace FlooringMastery.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        OrderOperations ops = new OrderOperations();
        ErrorLog log = new ErrorLog();
        private string formattedDate;
        private int orderNumber;
        private Order _currentOrder;

        public void Execute()
        {
            DisplayOrderWorkflow dowf = new DisplayOrderWorkflow();
            formattedDate = dowf.GetOrderDateFromUser();
            orderNumber = dowf.GetOrderNumberFromUser();
            _currentOrder = dowf.DisplayOrderInformation(formattedDate, orderNumber);

            ConfirmDeletion();
        }

        public void ConfirmDeletion()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Beep();
            Console.Beep();
            Console.Beep();

            Console.WriteLine("\tARE YOU SURE YOU WANT TO DELETE THIS ORDER?  (Y)es or (N)o");
           
            string input = Console.ReadLine().ToUpper();

            if (input == "Y")
            {
                OrderOperations ops = new OrderOperations();
                Response response = ops.OrderToDelete(formattedDate, orderNumber);

                if (response.Success)
                {
                    Console.WriteLine(response.Message);
                    
                }
                else
                {
                    Console.WriteLine(response.Message);
                }
            }

            else
            {
                MainMenu mainMenu = new MainMenu();
                mainMenu.Execute();
            }
        }

        

    }
}
