using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Data.DataRepositories;

namespace FlooringMastery.BLL
{
    public class OrderOperations
    {
        public  void ReadAllSettings()
        {

            

            var appSettings = ConfigurationManager.AppSettings;

            if (appSettings.Count == 0)
            {
                Console.WriteLine("AppSettings is empty.");
            }
            else
            {
                foreach (var key in appSettings.AllKeys)
                {
                    Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                }
            }

        }

        public  void AccessData()
        {
            TestDataRepository testDataRepository = new TestDataRepository();
            Console.WriteLine(testDataRepository.GetDataInformation());
        }

    }
}
