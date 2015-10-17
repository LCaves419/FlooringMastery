using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data.DataRepositories
{
    public class ProdRepository : IDataRepository
    {
        private const string _filePath = @"DataFiles\ProdFile\";

        private string file;

        //converts datetime to string format
        public List<Order> GetDataInformation(string file)
        {
            throw new NotImplementedException();
        }

        public string GetOrderFile(DateTime OrderDate)
        {
            var newOrderDate = _filePath + "Orders_" + OrderDate.ToString("MMddyyyy") + ".txt";
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
        public Order GetOrderNumber(string formattedOrderNumber, int OrderNumber)
        {
            List<Order> orders = GetDataInformation(formattedOrderNumber, OrderNumber);
            return orders.FirstOrDefault(a => a.OrderNumber == OrderNumber);
        }

        public void WriteNewLine(Order order, string formattedDate)
        {
            throw new NotImplementedException();
        }

        //if date folder exists we need to add new order to it
        public void WriteNewLine(Order order, string formattedDate, int OrderNumber)
        {
            var orders = GetDataInformation(formattedDate, OrderNumber);
            int newOrderNo = orders.Max(o => o.OrderNumber);
            int newOrderNo1 = newOrderNo + 1;

            using (var writer = File.AppendText(formattedDate))
            {
                writer.Write("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", newOrderNo1, order.LastName,
                    order.State, order.TaxRate, order.ProductType, order.Area, order.CostSqFt,
                    order.LaborSqFt, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
                //OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,
                //LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total
                //1,Wise,OH,6.25,Wood,100.00,5.15,4.75,515.00,475.00,61.88,1051.88
            }
        }

        public string CreateFile(DateTime currentDate)
        {
            string formattedDate = GetOrderFile(currentDate);
            //var newDateFile = _filePath + "Orders_" + formattedDate.ToString("MMddyyyy") + ".txt";
            using (StreamWriter writer = new StreamWriter(formattedDate))
            {
                writer.Write("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", "OrderNumber", "CustomerName", "State", "TaxRate", "ProductType", "Area", "CostPerSquareFoot",
                "LaborCostPerSquareFoot", "MaterialCost", "LaborCost", "Tax", "Total");
            }
            return formattedDate;
        }

        public decimal GetStateTaxRate(string state)
        {
            throw new NotImplementedException();
        }

        public decimal GetCostPerSqFt(string prodctType)
        {
            throw new NotImplementedException();
        }

        public decimal GetLaborPerSquareFt(string productType)
        {
            throw new NotImplementedException();
        }

        public int DeleteOrder(string formattedDate, int orderNumber)
        {
            throw new NotImplementedException();
        }

        void IDataRepository.GetEditedOrder(string formattedDate, int orderNumber, Order order)
        {
            throw new NotImplementedException();
        }

        public Order SortNewEditedFile(string formattedDate, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public Order GetEditedOrder(string formattedDate, int orderNumber, Order order)
        {
            throw new NotImplementedException();
        }

        bool IDataRepository.DeleteOrder(string formattedDate, int orderNumber)
        {
            throw new NotImplementedException();
        }

        //string IDataRepository.GetStateTaxRate(string state)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
