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
            bool isValid = false;
            decimal stateRate = 0;

            Console.WriteLine("Enter Account Information");
            Console.WriteLine("----------------------");
           
            Console.Write("Last Name: ");
            order.LastName = Console.ReadLine();

            do
            {
                Console.Write("State: ");
                order.State = Console.ReadLine();
                //string state = order.State;
                if (order.State.Length < 2)
                {
                    Console.WriteLine("That was not a valid entry.\n Please enter a state...");
                }
                else
                {
                    isValid = true;
                    stateRate = ops.MatchState(order.State);
                }
            } while (!isValid);

            //setting tax rate based on state
            order.TaxRate = stateRate;

            Console.WriteLine("Your tax rate is {0}: ", order.TaxRate);


            //-------------PRODUCT-----------------------

            Product product = new Product();
            do
            {
                //Console.WriteLine("");
                Console.WriteLine("Plese enter a product type: Carpet, Laminate, Tile, Wood: ");
                
                order.ProductType = Console.ReadLine();

               
                if (order.ProductType.Length > 8 || order.ProductType.Length < 1)
                {
                    Console.WriteLine("That was not a valid entry.\n Please enter a product...");
                }
                else
                {
                    
                     order.CostSqFt = ops.ReturnCostPerSquareFoot(order.ProductType);
                    
                }
            } while (order.CostSqFt == 0);// is 0

            Console.WriteLine("Your cost per square foot is {0:c}: ", order.CostSqFt);

            // getting labor per square foot
            order.LaborSqFt = ops.LaborPerSquareFt(order.ProductType);

            Console.WriteLine("Your labor per square foot is {0:c}: ", order.LaborSqFt);

            // getting area 
            Console.Write("Area: ");
            order.Area = decimal.Parse(Console.ReadLine());
            do
            {
                if (order.Area <= 0)
                {
                    Console.WriteLine("You need to get a bigger house!");
                    //error log
                }
            } while (order.Area <= 0);

           

            // getting material cost
            order.MaterialCost = ops.MaterialCost(order.ProductType, order.Area);

            Console.Write("Material Cost: {0:c} ", order.MaterialCost);
          
            //getting labor cost

            order.LaborCost = ops.LaborCost(order.ProductType, order.Area);
            Console.Write("\nLabor Cost: {0:c} ", order.LaborCost); 
           
            //get tax
            
            order.Tax = ops.Tax(order.State, order.MaterialCost);
            Console.Write("\nTax: {0:c} ", order.Tax);

            //get total
            Console.Write("\nTotal: {0:c}", order.Total);  
            order.Total =  ops.Total(order.MaterialCost, order.Tax, order.LaborCost);

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

