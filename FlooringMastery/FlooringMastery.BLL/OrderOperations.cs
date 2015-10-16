using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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

        public string GetOrderDate(DateTime OrderDate)
        {
            List<Order> order = new List<Order>();
            Response response = new Response();
            string newFileName = _repo.GetOrderFile(OrderDate);

            if (File.Exists(newFileName))
            {

                return newFileName;


            }
            // Console.WriteLine("That date does not match a a date in our files");
            //write this error to the error log
            return "That date does not match a a date in our files";
        }



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
                response.Message = "This is not the Date you are looking for...";
            }

            return response;
        }

        public void CreateOrder(Order order, string formattedDate)
        {
            Response response = new Response();
            _repo.WriteNewLine(order, formattedDate);
        }

        //checking to see if there is already a date file
        /// <summary>
        /// 
        /// </summary>
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
            Response response = new Response();
           
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

    public Order EditOrder(string formattedDate, int orderNumber, Order changedOrder)
    {
        Response response = new Response();
        _repo.GetEditedOrder(formattedDate, orderNumber, changedOrder);
        Order revisedOrder = _repo.SortNewEditedFile(formattedDate, orderNumber);
        // _repo.EditSameLine(formattedDate, orderNumber);

        return revisedOrder;
    }

}


}



