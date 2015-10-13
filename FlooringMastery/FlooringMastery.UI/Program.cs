using System.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Models;

namespace FlooringMastery.UI
{
   class Program
    {
         static void Main(string[] args)
        {

            OrderRepository repo = new OrderRepository();

             Console.WriteLine(repo.GetAllOrders());
        }
    }
}

       
  

