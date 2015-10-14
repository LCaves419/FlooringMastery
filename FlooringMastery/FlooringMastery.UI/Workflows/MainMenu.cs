using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class MainMenu
    {

        public void Execute()
        {
            string input = "";
            do
            {

                Console.Clear();
                Console.WriteLine("*****************************");
                Console.WriteLine("Flooring Program");
                Console.WriteLine();
                Console.WriteLine("1.  Display Orders");
                Console.WriteLine("2.  Add an Order");
                Console.WriteLine("3.  Edit an Order");
                Console.WriteLine("4.  Remove an Order");
                Console.WriteLine("5.  Quit");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("*****************************");
                Console.Write("Enter Choice:  ");

                input = Console.ReadLine();

                if (input != "5")
                {
                    ProcessChoice(input);
                }

            } while (input != "5");

        }

        private void ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    DisplayOrderWorkflow dowf = new DisplayOrderWorkflow();
                    dowf.Execute();
                   
                    break;
                case "2":
                    Console.WriteLine("This feature is not implemented yet!");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("This feature is not implemented yet!");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    break;
                case "4":
                    Console.WriteLine("This feature is not implemented yet!");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    break;
               default:
                    Console.WriteLine("{0} is an invalid entry!", choice);
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
