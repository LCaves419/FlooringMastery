using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.DataRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.Data
{
    public class DataFactory
    {
        private static string _mode = ConfigurationManager.AppSettings["Options"];

        public static IDataRepository CreateDataRepository()
        {
            switch (_mode)
            {
                case "Test":
                    return new TestRepository();
                    break;
                case "Prod":
                    return new ProdRepository();
                    break;
                default:
                    throw new NotSupportedException(String.Format("{0} not supported", _mode));
            }
        }
    }
}
