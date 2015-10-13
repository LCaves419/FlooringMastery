using System.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;

namespace FlooringMastery.UI
{
    internal class Program
    {
        private static void Main(string[] args)
        {

           OrderOperations orderOperations = new OrderOperations();
           // orderOperations.ReadAllSettings();
            orderOperations.AccessData();
            Console.ReadLine();

        }
    }
}
       
  

