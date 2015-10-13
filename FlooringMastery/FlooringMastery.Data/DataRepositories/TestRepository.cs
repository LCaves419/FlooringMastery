using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data.DataRepositories
{
    public class TestRepository : IDataRepository
    {
        private const string _filePath = @"DataFile\TestFiles";


        public string GetOrderDate(DateTime OrderDate)
        {
            List<Order> orders = new List<Order>();

            var newOrderDate = _filePath + "Orders_" + OrderDate + ".txt";

            if (File.Exists(newOrderDate))
            {
                return newOrderDate;
            }
            
                Console.WriteLine("That date does not match a a date in our files");
                //wwrite this error to the error log
                return null;
               
        }

        public List<Order> GetDataInformation(DateTime OrderDate)
        {
           //we are getting all the orders of a specific date that exists in our files 
           var orderFile =  GetOrderDate(OrderDate);
            List<Order> orders = new List<Order>();

            //read all orders that occur in orderFile, ie. on a specified date
            var reader = File.ReadAllLines(orderFile);

            //i = 1 starts on line 1 not 0.
            for (int i = 1; i < reader.Length; i++)
            {
                var columns = reader[i].Split(',');

                var order = new Order();

                order.OrderNumber = int.Parse(columns[0]);
                order.LastName = columns[1];
                order.State = columns[2];
                order.TaxRate = decimal.Parse(columns[3]);
                order.ProductType = (columns[4]);
                order.Area = decimal.Parse(columns[5]);
                order.CostSqFt = decimal.Parse(columns[6]);
                order.LaborSqFt = decimal.Parse(columns[7]);
                order.MaterialCost = decimal.Parse(columns[8]);
                order.LaborCost = decimal.Parse(columns[9]);
                order.Tax = decimal.Parse(columns[10]);
                order.Total = decimal.Parse(columns[11]);
                
            }

            return orders;

        }
    }
}

