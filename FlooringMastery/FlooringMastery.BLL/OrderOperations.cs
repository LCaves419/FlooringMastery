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

        public Response CreateOrder(string formattedDate)
        {
            Order newOrder = new Order();
            Response response = new Response();

            int orderNum = newOrder.OrderNumber = 0;

             _repo.WriteNewLine(newOrder,formattedDate,orderNum);
            return response;


        }

        //checking to see if there is already a date file
        public string CheckFileDate(DateTime currentDate)
        {
           string formattedDate = _repo.GetOrderFile(currentDate);
            List<Order> order = new List<Order>();
            Response response = new Response();


            if (File.Exists(formattedDate))
            {

                response = CreateOrder(formattedDate);
                return formattedDate;

            }
            else
            {


                // Console.WriteLine("That date does not match a a date in our files");
                //write this error to the error log

                string formattedDateNew = _repo.CreateFile(currentDate);
                return formattedDateNew;
            }
        }

    }
   
}

