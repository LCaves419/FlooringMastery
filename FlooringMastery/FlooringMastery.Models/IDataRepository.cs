﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public interface IDataRepository
    {
        List<Order> GetDataInformation(string file);
        string GetOrderFile(DateTime OrderDate);
        Order GetOrderNumber(string formattedOrderNumber, int OrderNumber);
        void WriteNewLine(Order order, string formattedDate);
        string CreateFile(DateTime currentDate);
        bool DeleteOrder(string formattedDate, int orderNumber);
    }
}
