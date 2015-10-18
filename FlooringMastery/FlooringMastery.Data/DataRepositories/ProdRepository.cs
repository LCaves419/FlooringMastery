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
        private const string _filePath = @"DataFiles\TestFiles\";
        private const string _stateFile = @"DataFiles\TestFiles\State.txt";
        private const string _prodFile = @"DataFiles\TestFiles\Products.txt";
        private string file;
        //DataFiles\TestFiles\    .txt
        //converts datetime to string format

        public string GetOrderFile(DateTime OrderDate)
        {
            var newOrderDate = _filePath + "Orders_" + OrderDate.ToString("MMddyyyy") + ".txt";

            file = newOrderDate;
            return newOrderDate;
            // _filePath.FirstOrDefault(a => a. == file);
        }

        //returns all orders of a specific date
        public List<Order> GetDataInformation(string file)
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
        public Order GetOrderNumber(string formattedDate, int OrderNumber)
        {
            List<Order> orders = GetDataInformation(formattedDate);
            return orders.FirstOrDefault(a => a.OrderNumber == OrderNumber);
        }

        //if date folder exists we need to add new order to it
        public void WriteNewLine(Order order, string formattedDate)
        {
            List<Order> orders = GetDataInformation(formattedDate);
            int newOrderNo = 1;
            if (orders.Count > 1)
            {
                newOrderNo = orders.Max(o => o.OrderNumber) + 1;
            }
            order.OrderNumber = newOrderNo;
            using (var writer = File.AppendText(formattedDate))
            {
                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber, order.LastName,
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
                WriteHeader(writer);
                //go from here to BLL 
            }
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

        public bool DeleteOrder(string formattedDate, int orderNumber)
        {
            List<Order> orders = GetDataInformation(formattedDate);
            //string FileOnly = formattedDate.Substring(formattedDate.Length - 19);
            var ordersToKeep = from o in orders
                where o.OrderNumber != orderNumber
                select o;
            //if result.count > 0 
            if (ordersToKeep.Count() > 1)
            {
                RewriteFile(formattedDate, ordersToKeep);
                return true;
            }
            else if (ordersToKeep.Count() == 0)
            {
                System.IO.File.Delete(formattedDate);
                return true;
            }
            return false;
            //    try
            //    {
            //        System.IO.File.Delete(formattedDate);
            //    }
            //    catch (System.IO.IOException e)
            //    {
            //        Console.WriteLine(e.Message);
            //        return false;
            //    }

            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public void GetEditedOrder(string formattedDate, int OrderNumber, Order changedOrder)
        {
            //public Order GetOrderNumber(string formattedDate, int OrderNumber)

            List<Order> orders = GetDataInformation(formattedDate); //brings back all orders from file

            var otherOrders = from o in orders
                where o.OrderNumber != OrderNumber
                // gets all orders EXCEPT the one we are changing
                select o;

            //File.WriteAllLines(formattedDate, result.ToString());//rewrites to file all orders EXCEPT the original orderNumber

            RewriteFile(formattedDate, otherOrders);
            using (var writer = File.AppendText(formattedDate)) // appends new order to end of file
            {
                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", changedOrder.OrderNumber,
                    changedOrder.LastName, changedOrder.State, changedOrder.TaxRate, changedOrder.ProductType,
                    changedOrder.Area,
                    changedOrder.CostSqFt,
                    changedOrder.LaborSqFt, changedOrder.MaterialCost, changedOrder.LaborCost, changedOrder.Tax,
                    changedOrder.Total);
            }
        }

        private static void RewriteFile(string formattedDate, IEnumerable<Order> result)
        {
            using (StreamWriter writer = new StreamWriter(formattedDate))
            {
                WriteHeader(writer);
            }

            foreach (var o in result)
            {
                using (var writer = File.AppendText(formattedDate))
                {
                    WriteOrder(writer, o);
                }
            }
        }

        private static void WriteOrder(StreamWriter writer, Order o)
        {
            writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", o.OrderNumber,
                o.LastName, o.State, o.TaxRate, o.ProductType,
                o.Area,
                o.CostSqFt,
                o.LaborSqFt, o.MaterialCost, o.LaborCost, o.Tax,
                o.Total);
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
