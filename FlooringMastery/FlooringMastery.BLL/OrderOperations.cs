using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Data.DataRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.BLL
{
    public class OrderOperations
    {
        // private DateTime orderDate;
        private IDataRepository _repo;

        //our constructor
        public OrderOperations()
        {
            _repo = DataFactory.CreateDataRepository();

        }
        /// <summary>
        /// validates the order date exists
        /// </summary>
        /// <param name="OrderDate"></param>
        /// <returns></returns>
        public string GetOrderDate(DateTime OrderDate)
        {
            List<Order> order = new List<Order>();
            Response response = new Response();
            string newFileName = _repo.GetOrderFile(OrderDate);


            if (File.Exists(newFileName))
            {
                response.Success = true;
                return newFileName;
            }
            else
            {
                response.Success = false;
                response.Message = "This is not the order date you are looking for...";
                return null;
            }
        }

        public List<Order> GetAllOrders(string formattedDate)
        {
            List<Order> allOrders = _repo.GetDataInformation(formattedDate);
            return allOrders;
        }


        /// <summary>
        /// brings back an order using the order number, validates the actual order number exists
        /// </summary>
        /// <param name="newFileName"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public Response GetOrder(string newFileName, int orderNumber)
        {
            var response = new Response();
            var order = _repo.GetOrderNumber(newFileName, orderNumber);


            if (order != null)
            {
                response.Success = true;
                response.OrderInfo = order;
            }
            else
            {
                response.Success = false;
                response.Message = "This is not the order number you are looking for...";
            }

            return response;
        }
        public void CreateOrder(Order order, string formattedDate)
        {
            Response response = new Response();
            _repo.WriteNewLine(order, formattedDate);
        }


        /// <summary>
        /// 
        /// </summary>checks to see if a date file already exists
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public string CheckFileDate(DateTime currentDate)
        {
            string formattedDate = _repo.GetOrderFile(currentDate);
            List<Order> order = new List<Order>();
            Response response = new Response();
            Order newOrder = new Order();

            if (!File.Exists(formattedDate))
            {
                //newOrder.OrderNumber = 1;
                string formattedDateNew = _repo.CreateFile(currentDate);
                return formattedDateNew;
            }

            else
            {
                //_repo.WriteNewLine(newOrder, formattedDate);
                return formattedDate;

            }

            // Console.WriteLine("That date does not match a a date in our files");
            //write this error to the error log


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="formattedDate"></param>
        /// <returns></returns>
        public Response NewCustomerInformationResponse(Order order, string formattedDate)
        {
            var response = new Response();

            _repo.WriteNewLine(order, formattedDate);
            response.OrderInfo = order;

            return response;

        }


        public decimal MatchState(string state)
        {
            bool isValid = false;
            decimal taxRate = 0;
            string abbr = "";
            do
            {

                string upperState = state.ToUpper();

                if (upperState.Length == 2)
                {
                    switch (upperState)
                    {
                        case "OH":
                            isValid = true;
                            break;
                        case "PA":
                            isValid = true;
                            break;
                        case "MI":
                            isValid = true;
                            break;
                        case "IN":
                            isValid = true;
                            break;
                        default:
                            Console.WriteLine("That was not a valid state.");
                            Console.WriteLine("Please reenter state...");
                            break;
                    }

                    taxRate = _repo.GetStateTaxRate(upperState);
                    return taxRate;
                }

                switch (upperState)
                {
                    case "OHIO":
                        abbr = "OH";
                        isValid = true;
                        break;
                    case "PENNSYLVANIA":
                        abbr = "PA";
                        isValid = true;
                        break;
                    case "MICHIGAN":
                        abbr = "MI";
                        isValid = true;
                        break;
                    case "INDIANA":
                        abbr = "IN";
                        isValid = true;
                        break;
                    default:
                        Console.WriteLine("That was not a valid state.");
                        Console.WriteLine("Please reenter state...");
                        break;
                }

            } while (!isValid);
            taxRate = _repo.GetStateTaxRate(abbr);

            return taxRate;
        }


        public decimal ReturnCostPerSquareFoot(string ProductType)
        {

            decimal costPerSqFt = 0;
            string upperProduct = ProductType.ToUpper();
            bool isValid = false;

            switch (upperProduct)
            {

                case "CARPET":
                    isValid = true;
                    break;

                case "LAMINATE":
                    isValid = true;
                    break;
                case "TILE":
                    isValid = true;
                    break;
                case "WOOD":
                    isValid = true;
                    break;
                default:

                    Console.WriteLine("That was not a valid product type.");
                    return costPerSqFt = 0;
            }
            costPerSqFt = _repo.GetCostPerSqFt(upperProduct);

            return costPerSqFt;
        }

        public decimal LaborPerSquareFt(string upperProduct)
        {

            decimal laborPerSquareFt = _repo.GetLaborPerSquareFt(upperProduct);
            return laborPerSquareFt;
        }

        public decimal MaterialCost(string upperProduct, decimal area)
        {
            decimal costPerSquareFt = _repo.GetCostPerSqFt(upperProduct);

            decimal materialCost = (costPerSquareFt * area);
            return materialCost;
        }

        public decimal LaborCost(string upperProduct, decimal area)
        {
            decimal laborCostPerSquareFt = _repo.GetLaborPerSquareFt(upperProduct);
            decimal laborCost = area * laborCostPerSquareFt;

            return laborCost;
        }

        public decimal Tax(string state, decimal materialCost)
        {
            decimal taxRate = _repo.GetStateTaxRate(state);
            decimal newTaxRate = taxRate / 100;
            decimal tax = materialCost * newTaxRate;

            return tax;
        }

        public decimal Total(decimal materialCost, decimal tax, decimal laborCost)
        {
            decimal total = (materialCost * tax) + laborCost;
            return total;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formattedDate"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public Response OrderToDelete(string formattedDate, int orderNumber)
        {
            var response = new Response();

            // Order deletedOrder = _repo.GetOrderNumber(formattedDate, orderNumber);
            bool deletedNum = _repo.DeleteOrder(formattedDate, orderNumber);

            if (deletedNum)
            {
                response.Success = true;
                response.Message = "The order you were trying to delete has been successfully deleted.";

            }
            else
            {
                //need to do as TRY CATCH ---IF reach here wil have entered something other than an order num
                response.Success = false;
                response.Message = "You were not able to delete that order.";
            }

            return response;
        }

        public Response EditOrder(string formattedDate, int orderNumber, Order changedOrder)
        {
            Response response = new Response();
            _repo.GetEditedOrder(formattedDate, orderNumber, changedOrder);
            var revisedOrder = _repo.SortNewEditedFile(formattedDate, orderNumber);

            if (revisedOrder != null)
            {
                response.Success = true;
                response.OrderInfo = revisedOrder;
            }
            else
            {
                response.Success = false;
                response.Message = "The Edit was not sucessful in operations/EditOrder.";
            }
            response.OrderInfo = revisedOrder;
            return response;
        }

        public void CallingErrorLogRepository(string strgMsg)
        {
            ErrorLogRepository log = new ErrorLogRepository();
            log.MyLogFile(strgMsg);
        }


    }
}



