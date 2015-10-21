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
         private static List<Order> _orders = new List<Order>();

    public TestRepository()
    {
        if (!_orders.Any())
        {
            _orders.AddRange(new List<Order>()
                {
                    new Order
                    {
                        OrderNumber = 1, LastName = "Lin", State = "OH", TaxRate =(decimal)6.25, ProductType = "Wood",
                        Area =(decimal)100, CostSqFt = (decimal)5.15, LaborSqFt = (decimal)4.75, MaterialCost = (decimal)515.00, 
                        LaborCost = (decimal)475.00, Tax = (decimal)61.88, Total = (decimal)1051.88
                    },
                    new Order
                    {
                        OrderNumber = 2, LastName = "Shaw", State = "MI", TaxRate =(decimal)5.25, ProductType = "Tile",
                        Area =(decimal)100, CostSqFt = (decimal)5.15, LaborSqFt = (decimal)4.75, MaterialCost = (decimal)515.00,
                        LaborCost = (decimal)475.00, Tax = (decimal)61.88, Total = (decimal)1051.88}
                });
        }
}

        public void WriteNewLine(Order order, string formattedDate)
        {
            order.OrderNumber = (_orders.Any()) ? _orders.Max(c => c.OrderNumber) + 1 : 1;

            _orders.Add(order);
        }


        public bool DeleteOrder(string formattedDate, int orderNumber)
        {
            _orders.RemoveAll(c => c.OrderNumber == orderNumber);
            return true;
        }

        public void GetEditedOrder(string formattedDate, int OrderNumber, Order order)
        {
            DeleteOrder(formattedDate, order.OrderNumber);
            _orders.Add(order);
        }

        public Order GetOrderNumber(string formattedOrderNumber, int orderNumber)
        {
            return _orders.FirstOrDefault(c => c.OrderNumber == orderNumber);
        }



        //Methods not using
        //---------------------------------------------------------------------------------------------------------



        //---------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------
    

    //Dictionary<int, Order> mockOrder = new Dictionary<int, Order>();

    //private const string _filePath = @"DataFiles\TestFiles\Orders_01012020.txt";
    private const string _stateFile = @"DataFiles\TestFiles\State.txt";
    private const string _prodFile = @"DataFiles\TestFiles\Products.txt";
    //private string file;
    ////DataFiles\TestFiles\    .txt
    ////converts datetime to string format


    public string GetOrderFile(DateTime OrderDate)
    {
        string fakeDate = @"DataFiles\TestFiles\Orders_01012020.txt";
        return fakeDate;
    }


        public List<Order> GetDataInformation(string file)
        {
            
            return _orders;
        }

        public string CreateFile(DateTime currentDate)
        {
            string formattedDate = GetOrderFile(currentDate);
            //var newDateFile = _filePath + "Orders_" + formattedDate.ToString("MMddyyyy") + ".txt";
            using (StreamWriter writer = new StreamWriter(formattedDate))
            
            return formattedDate;
        }

        private static void WriteHeader(StreamWriter writer)
        {
            writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", "OrderNumber",
                "CustomerName", "State", "TaxRate", "ProductType", "Area", "CostPerSquareFoot",
                "LaborCostPerSquareFoot", "MaterialCost", "LaborCost", "Tax", "Total");
        }

        public decimal GetStateTaxRate(string state)
        {
            List<State> states = new List<State>();
            var reader = File.ReadAllLines(_stateFile);

            //i = 1 starts on line 1 not 0.
            for (int i = 1; i < reader.Length; i++)
            {
                var newState = new State();
                var columns = reader[i].Split(',');
                newState.StateAbbreviation = columns[0];
                newState.StateName = columns[1];
                newState.TaxRate = decimal.Parse(columns[2]);
                states.Add(newState);
            }
            var result =
                states.FirstOrDefault(
                    s => string.Equals(s.StateAbbreviation, state, StringComparison.CurrentCultureIgnoreCase));
            return result?.TaxRate ?? 0;
        }

        public decimal GetCostPerSqFt(string productType)
        {
            List<Product> products = new List<Product>();
            var reader = File.ReadAllLines(_prodFile);

            //i = 1 starts on line 1 not 0.
            for (int i = 1; i < reader.Length; i++)
            {
                var newProduct = new Product();
                var columns = reader[i].Split(',');
                newProduct.ProductType = columns[0];
                newProduct.CostPerSquareFoot = decimal.Parse(columns[1]);
                products.Add(newProduct);
            }

            var result =
                products.FirstOrDefault(
                    p => string.Equals(p.ProductType, productType, StringComparison.CurrentCultureIgnoreCase));
            return result?.CostPerSquareFoot ?? 0;
        }

        public decimal GetLaborPerSquareFt(string productType)
        {
            List<Product> products = new List<Product>();
            var reader = File.ReadAllLines(_prodFile);

            //i = 1 starts on line 1 not 0.
            for (int i = 1; i < reader.Length; i++)
            {
                var newProduct = new Product();
                var columns = reader[i].Split(',');
                newProduct.ProductType = columns[0];
                newProduct.CostPerSquareFoot = decimal.Parse(columns[1]);
                newProduct.LaborCostPerSquareFoot = decimal.Parse((columns[2]));
                products.Add(newProduct);
            }

            var result =
                products.FirstOrDefault(
                    p => string.Equals(p.ProductType, productType, StringComparison.CurrentCultureIgnoreCase));
            return result?.LaborCostPerSquareFoot ?? 0;
        }

        private static void RewriteFile(string formattedDate, IEnumerable<Order> result)
        {
            
        }

        private static void WriteOrder(StreamWriter writer, Order o)
        {
           
        }

        public Order SortNewEditedFile(string formattedDate, int orderNumber)
        {
            List<Order> orders = GetDataInformation(formattedDate); //brings back all orders from file
            var sortedOrders = orders.OrderBy(o => o.OrderNumber);

            using (StreamWriter writer = new StreamWriter(formattedDate))
            {
                WriteHeader(writer);
            }

            foreach (var order in sortedOrders)
            {
                using (var writer = File.AppendText(formattedDate)) // appends new order to end of file  
                {
                    WriteOrder(writer, order);
                }
            }
            Order revisedOrder = sortedOrders.FirstOrDefault(a => a.OrderNumber == orderNumber);

            return revisedOrder;
        }

    }


}

