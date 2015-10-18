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
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\t*************************");
                Console.WriteLine("\t*************************");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\t     WELCOME TO SG CORP \n");
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("\t*************************");
                Console.WriteLine("\t*************************");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\t        MAIN MENU");
                Console.ForegroundColor = ConsoleColor.DarkBlue;


                Console.WriteLine("\t*************************");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine();
                Console.WriteLine("\t1.  Display Orders");
                Console.WriteLine("\t2.  Add an Order");
                Console.WriteLine("\t3.  Edit an Order");
                Console.WriteLine("\t4.  Remove an Order");
                Console.WriteLine("\t5.  Quit");
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("\t*************************");
                Console.WriteLine("\t*************************");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("\t   Enter Choice:  ");

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
                    Console.ReadLine();
                    break;
                case "2":
                    AddOrderWorkflow aowf = new AddOrderWorkflow();
                    aowf.Execute();
                    Console.ReadLine();
                    break;
                case "3":
                   EditOrderWorkflow eowf = new EditOrderWorkflow();
                    eowf.Execute();
                    Console.ReadLine();
                    break;
                case "4":
                   RemoveOrderWorkflow rowf = new RemoveOrderWorkflow();
                    rowf.Execute();
                    Console.ReadLine();
                    break;
               default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} is an invalid entry!", choice);
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
