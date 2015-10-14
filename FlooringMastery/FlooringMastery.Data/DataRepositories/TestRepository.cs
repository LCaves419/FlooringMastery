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
        private const string _filePath = @"DataFiles\TestFiles\";

        private string file;

        //converts datetime to string format
        public string GetOrderFile(DateTime OrderDate)
        {
             var newOrderDate =  _filePath + "Orders_" + OrderDate.ToString("MMddyyyy") + ".txt";
            file = newOrderDate;
            return newOrderDate;
            //return _filePath.FirstOrDefault(a => a. == orderNumber);

        }


        //returns all orders of a specific date
        public List<Order> GetDataInformation(string file, int OrderNumber)
        {
           List<Order> orders = new List<Order>();

            //read all orders that occur in orderFile, ie. on a specified date
            var reader = File.ReadAllLines(file);

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

                orders.Add(order);
                
            }
            return orders;

        }

        //finds correct order number from file
        public Order GetOrderNumber( string formattedOrderNumber, int OrderNumber)
        {
            List<Order> orders = GetDataInformation(formattedOrderNumber, OrderNumber);
            return orders.FirstOrDefault(a => a.OrderNumber == OrderNumber);
        }

        //if date folder exists we need to add new order to it
        public void WriteNewLine(Order order, string formattedOrderNumber, int OrderNumber)
        {
            var orders = GetDataInformation(formattedOrderNumber, OrderNumber);
            int newOrderNo = orders.Max(o => o.OrderNumber);
            int newOrderNo1 = newOrderNo +1;

            using (var writer = File.AppendText(_filePath))
            {
                writer.WriteLine("{0},{1},{2},{3}",, account.FirstName,
                    account.LastName, account.Balance);
            }
        }
    }
}

