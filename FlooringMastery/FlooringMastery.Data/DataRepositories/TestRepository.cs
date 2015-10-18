using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FlooringMastery.Models;

namespace FlooringMastery.Data.DataRepositories
{
    public class TestRepository : IDataRepository
    {
        private static List<Order> mockOrders = new List<Order>();

        public TestRepository()
        {
          mockOrders.AddRange(new List<Order>() {
                new Order
                {
                    OrderNumber = 1, LastName = "Victor Pudelski", State = "OH", TaxRate = (decimal)6.25,
                    ProductType = "Wood", Area = 100, CostSqFt = (decimal)1.12, LaborSqFt = (decimal)2.22,
                    MaterialCost = (decimal)30.00, LaborCost = (decimal)50.00, Tax = (decimal)40.00, Total = (decimal)1200.00
                },
                new Order
                {
                    OrderNumber = 2,
                    LastName = "Victor Pudelski",State = "MI",TaxRate = (decimal)6.25,ProductType = "Wood",
                    Area = 100, CostSqFt = (decimal)1.12,LaborSqFt = (decimal)2.22,MaterialCost = (decimal)30.00,
                    LaborCost = (decimal)50.00,Tax = (decimal)40.00,Total = (decimal)1200.00}
                });
        }
     
        public bool DeleteOrder(string formattedDate, int orderNumber)
        {
            mockOrders.RemoveAll(c => c.OrderNumber == orderNumber);
            return true;
        }

        public void GetEditedOrder(string formattedDate, int OrderNumber, Order changedOrder)
        {
            DeleteOrder(formattedDate, changedOrder.OrderNumber);
            mockOrders.Add(changedOrder);
        }
        //finds correct order number from file
        public Order GetOrderNumber(string formattedDate, int OrderNumber)
        {
            return mockOrders.FirstOrDefault(a => a.OrderNumber == OrderNumber);
        }
        //adding a new order to list
        public void WriteNewLine(Order order, string formattedDate)
        {
            order.OrderNumber = (mockOrders.Any()) ? mockOrders.Max(c => c.OrderNumber) + 1 : 1;
            mockOrders.Add(order);
        }
//**********************************************************************************************************
//*******************************************************************************************************
        public string CreateFile(DateTime currentDate)
        {
            return null;
        }

        private static void WriteHeader(StreamWriter writer)
        {
           
        }

        public decimal GetStateTaxRate(string state)
        {
            return 0;
        }

        public decimal GetCostPerSqFt(string productType)
        {
            return 0;
        }

        public decimal GetLaborPerSquareFt(string productType)
        {
            return 0;
        }
        private static void RewriteFile(string formattedDate, IEnumerable<Order> result)
        {
           
        }

        private static void WriteOrder(StreamWriter writer, Order o)
        {
            
        }

        public Order SortNewEditedFile(string formattedDate, int orderNumber)
        {
            return null;
        }

       public List<Order> GetDataInformation(string file)
        {
            
            return mockOrders;
        }
        public string GetOrderFile(DateTime OrderDate)
        {
            string hell = "hell";
            return hell;
        }

    }


}

