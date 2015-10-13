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

        public Response GetOrder(DateTime OrderDate, int orderNumber)
        {
            var response = new Response();
            var order = _repo.GetDataInformation(OrderDate);



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
