using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data.DataRepositories
{
    public interface IErrorLogRepository
    {
        /// <summary>
        /// The MyLogFile method is used to document details of each test run.
        /// </summary>
        void MyLogFile( string strMessage);
    }

    public  class ErrorLogRepository : IErrorLogRepository
    {
        private string fileName;

        public ErrorLogRepository()
        {
            fileName = @"DataFiles\TestFiles\ErrorLog.txt";
        }
        //public void WriteError(string fileName)
        //{
        //    this.fileName = fileName;
        //}
        ////Use LogFile to document the test run results
        /// <summary>
        /// The MyLogFile method is used to document details of each test run.
        /// </summary>
        public void MyLogFile( string strMessage)
        {
            // Store the script names and test results in a output text file.
            using (StreamWriter writer = new StreamWriter(new FileStream(fileName, FileMode.Append)))
            {
                DateTime date = DateTime.Now;
                writer.WriteLine("{0}{1}", strMessage, date);
            }
        }
    } 
}
    

