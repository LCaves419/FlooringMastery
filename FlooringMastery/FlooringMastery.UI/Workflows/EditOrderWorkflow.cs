using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.BLL;

namespace FlooringMastery.UI.Workflows
{
    public class EditOrderWorkflow
    {
        private string formattedDate;
        private int orderNumber;
        private Order _currentOrder;
        private Order changedOrder;

        public void Execute()
        {

            DisplayOrderWorkflow dowf = new DisplayOrderWorkflow();
            formattedDate = dowf.GetOrderDateFromUser();
            orderNumber = dowf.GetOrderNumberFromUser();
            //_the current order returns order before it's edited
            _currentOrder = dowf.DisplayOrderInformation(formattedDate, orderNumber);
            changedOrder =  EditElements(_currentOrder);

        }

        public Order EditElements(Order _currrentOrder)
        {
            OrderOperations ops = new OrderOperations();
            var response = new Response();
            Order order = new Order();
            Console.WriteLine("Press enter to accept the current information in each field.  To make changes enter new information.");
            Console.WriteLine("*******Order Number: {0}  ********", _currentOrder.OrderNumber);
            Console.Write("Last Name ({0}): ", _currentOrder.LastName);
            string inputN = Console.ReadLine();
            if (inputN != "")
            {
                _currentOrder.LastName = inputN;
                
            }
            else
            {
                _currentOrder.LastName = _currentOrder.LastName;
            }
           
            Console.Write("State ({0}): ", _currentOrder.State);
            string inputS = Console.ReadLine();
            if (inputS != "")
            {
                _currentOrder.State = inputS;
            }
            else
            {
                _currentOrder.State = _currentOrder.State;
            }

            Console.Write("Product Type ({0}): ", _currentOrder.ProductType);
            string inputP = Console.ReadLine();
            if (inputP != "")
            {
                _currentOrder.ProductType = inputP;
            }
            else
            {
                _currentOrder.ProductType = _currentOrder.ProductType;
            }

            //ops.CreateOrder(order, formattedDate);
            changedOrder = _currrentOrder;
            Order revisedOrder = ops.EditOrder(formattedDate, orderNumber, changedOrder);

            Console.Clear();
            Console.WriteLine("Here is your new order information:  ");
            DisplayOrderWorkflow dowf = new DisplayOrderWorkflow();
            dowf.PrintOrderInformation(revisedOrder);

            
            Console.WriteLine("Press enter for Main Menu...");
            return changedOrder;
        }
    }
    
}
