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
                string state = order.State;
                if (state.Length < 2)
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

            decimal costPerSquareFt = 0 ;
            Product product = new Product();


            do
            {
                //Console.WriteLine("");
                Console.WriteLine("Plese enter a product type: Carpet, Laminate, Tile, Wood: ");
                
                string floorProduct = Console.ReadLine();

               
                if (floorProduct.Length > 8 || floorProduct.Length < 1)
                {
                    Console.WriteLine("That was not a valid entry.\n Please enter a product...");
                }
                else
                {
                    
                     costPerSquareFt = ops.ReturnCostPerSquareFoot(floorProduct);
                }
            } while (costPerSquareFt == 0);// is 0

            
            Console.WriteLine("Your cost per square foot is {0}: ", costPerSquareFt);



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

