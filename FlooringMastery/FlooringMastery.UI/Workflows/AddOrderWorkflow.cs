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
        OrderOperations ops = new OrderOperations();
        ErrorLog log = new ErrorLog();

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
            bool isValid = false;
            decimal stateRate = 0;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine("\tEnter Account Information");
            Console.WriteLine("\t----------------------");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\tLast Name: ");
            order.LastName = Console.ReadLine();

            do
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("\tState: ");
                order.State = Console.ReadLine();
                //string state = order.State;
                if (order.State.Length < 2)
                {
                    Console.WriteLine("\tThat was not a valid entry.\n Please enter a state...");
                    log.ErrorMessage = "That was not a valid entry(state) UI:PopulateOrder/AddWorkflow....";
                    ops.CallingErrorLogRepository(log.ErrorMessage);
                }
                else
                {
                    isValid = true;
                    stateRate = ops.MatchState(order.State);
                }
            } while (!isValid);

            //setting tax rate based on state
            order.TaxRate = stateRate;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\tYour tax rate is {0}: ", order.TaxRate);


            //-------------PRODUCT-----------------------

            Product product = new Product();
            do
            {
                Console.ForegroundColor = ConsoleColor.White;

                //Console.WriteLine("");
                Console.Write("\tPlese enter a product type:\n\t\t Carpet, Laminate, Tile, Wood: ");
                
                order.ProductType = Console.ReadLine();

               
                if (order.ProductType.Length > 8 || order.ProductType.Length < 1)
                {
                    Console.WriteLine("\tThat was not a valid entry.\n Please enter a product...");
                    log.ErrorMessage = "That was not a valid entry (product) UI:PopulateOrder....";
                    ops.CallingErrorLogRepository(log.ErrorMessage);
                }
                else
                {
                    
                     order.CostSqFt = ops.ReturnCostPerSquareFoot(order.ProductType);
                    
                }
            } while (order.CostSqFt == 0);// is 0

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\tYour cost per square foot is {0:c}: ", order.CostSqFt);

            // getting labor per square foot
            order.LaborSqFt = ops.LaborPerSquareFt(order.ProductType);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\tYour labor per square foot is {0:c}: ", order.LaborSqFt);

            // getting area 
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\tArea: ");
            order.Area = decimal.Parse(Console.ReadLine());
            do
            {
                if (order.Area <= 0)
                {
                    Console.WriteLine("\tYou need to get a bigger house!");
                    log.ErrorMessage = "That was not a valid area (area) UI:PopulateOrder....";
                    ops.CallingErrorLogRepository(log.ErrorMessage);

                    //error log
                }
            } while (order.Area <= 0);

           

            // getting material cost
            order.MaterialCost = ops.MaterialCost(order.ProductType, order.Area);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\tMaterial Cost: {0:c} ", order.MaterialCost);

            //getting labor cost
            Console.ForegroundColor = ConsoleColor.White;

            order.LaborCost = ops.LaborCost(order.ProductType, order.Area);
            Console.Write("\nLabor Cost: {0:c} ", order.LaborCost); 
           
            //get tax
            Console.ForegroundColor = ConsoleColor.White;

            order.Tax = ops.Tax(order.State, order.MaterialCost);
            Console.Write("\nTax: {0:c} ", order.Tax);

            //get total
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\nTotal: {0:c}", order.Total);  
            order.Total =  ops.Total(order.MaterialCost, order.Tax, order.LaborCost);

            Console.WriteLine();

            ops.CreateOrder(order, formattedDate);

           Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n\tHere is your new order information:  \n");
            DisplayOrderWorkflow dowf = new DisplayOrderWorkflow();
            dowf.PrintOrderInformation(order);
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}

