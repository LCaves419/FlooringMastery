using System;
using System.Collections.Generic;
using System.Configuration;
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

        private IDataRepository _repo;

        //our constructor
        public OrderOperations()
        {
            _repo = DataFactory.CreateDataRepository();
        }

        public Response GetOrderDate(DateTime OrderDate)
        {
            
        } 
        public Response GetOrder(int OrderNumber)
        {
            
            var response = new Response();
            //TODO: write GetOrder method in data layer- not sure which repository to put this in
            var account = _repo.GetOrder(OrderNumber);

            if (account == null)
            {
                response.Success = false;
                response.Message = "This is not the account you are looking for...";
            }
            else
            {
                response.Success = true;
                //TODO: finish this method- change Account info to ordeinfo after we do date 
                //response.AccountInfo = account;
            }

            return response;
        }



        //var appSettings = ConfigurationManager.AppSettings;

        //if (appSettings.Count == 0)
        //{
        //    Console.WriteLine("AppSettings is empty.");
        //}
        //else
        //{
        //    foreach (var key in appSettings.AllKeys)
        //    {
        //        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
        //    }
        //}

    }

        //public Order AccessData(Order order)
        //{
        //    TestDataRepository testDataRepository = new TestDataRepository();
        //    return testDataRepository.GetDataInformation();
        //}

    }
}
