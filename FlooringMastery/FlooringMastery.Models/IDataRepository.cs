using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public interface IDataRepository
    {
        List<Order> GetDataInformation(int OrderNumber);
        string GetOrderFile(DateTime OrderDate);
    }
}
