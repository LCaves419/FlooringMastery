using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data.DataRepositories;
using FlooringMastery.Models;
using NUnit.Framework;

namespace FlooringMastery.Tests
{

    [TestFixture]
    public class OrderOperationsTests
    {
        
        [Test]
        public void ShouldReturn_AllOrders()
        {
            //Arrange
            var repo = new TestRepository();
            string fakeDate = @"DataFiles\TestFiles\Orders_01012020.txt";
            OrderOperations ops = new OrderOperations(repo);

            //Act

            List<Order> orders = ops.GetAllOrders(fakeDate);
            //Assert

            Assert.AreEqual(orders.Count, 2);
            Assert.AreEqual(orders.Select(o => o.OrderNumber).Contains(1), true);
        }

        [Test]
        public void GetOrder_ShouldReturn_True_IfOrderExists()
        {
            //Arrange
            var repo = new TestRepository();
            string fakeDate = @"DataFiles\TestFiles\Orders_01012020.txt";
            OrderOperations ops = new OrderOperations(repo);
            //Act
            Response response = ops.GetOrder(fakeDate, 2);

            //Assert
            Assert.AreEqual(true, response.Success);
        }

        [Test]
        public void GetOrder_ShouldReturn_False_IfOrder_DoesNotExist()
        {
            //Arrange
            var repo = new TestRepository();
            var errorLogRepository = new FakeErrorLogRepository();
            string fakeDate = @"DataFiles\TestFiles\Orders_01012020.txt";
            OrderOperations ops = new OrderOperations(repo, errorLogRepository);
            //Act
            Response response = ops.GetOrder(fakeDate, 5);

            //Assert
            Assert.AreEqual(false, response.Success);
            
        }

        [Test]
        public void MatchState_ShouldReturn_TaxRate()
        {
            //Arrange
            var repo = new TestRepository();
            var errorLogRepository = new FakeErrorLogRepository();
            string fakeDate = @"DataFiles\TestFiles\Orders_01012020.txt";
            OrderOperations ops = new OrderOperations(repo, errorLogRepository);
            //Act
            var taxRate = ops.MatchState("OH");

            //Assert
            Assert.AreEqual(6.25M, taxRate);

        }

    }

    public class FakeErrorLogRepository : IErrorLogRepository
    {
        public void MyLogFile(string strMessage)
        {
           
        }
    }
}

