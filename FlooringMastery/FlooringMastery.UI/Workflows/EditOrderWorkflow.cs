﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.BLL;

namespace FlooringMastery.UI.Workflows
{
    public class EditOrderWorkflow
    {
        private string formattedDate;
        private int orderNumber;
        private Order _currentOrder;

        //private string name;
        //private string state;
        //private decimal taxRate;
        //private decimal costPerSquareFt;
        //private decimal area;
        //private decimal materialCost;
        //private decimal laborPerSquareFt;
        //private decimal tax;
        //private string product;
        //private decimal laborCost;
        //private decimal total;


        public void Execute()
        {
            DisplayOrderWorkflow dowf = new DisplayOrderWorkflow();
            formattedDate = dowf.GetOrderDateFromUser();
            orderNumber = dowf.GetOrderNumberFromUser();
            //_the current order returns order before it's edited
            _currentOrder = dowf.DisplayOrderInformation(formattedDate, orderNumber);
            ChangeName();
            ChangeState();
            ChangeProduct();
            ChangeArea();
            ChangeMaterialCost();
            ChangeTax();
            ChangeLaborPerSquareFt();
            ChangeLaborCost();
            ChangeTotal();
            NewOrder();
        }

        public void ChangeName()
        {
            Console.WriteLine("Press enter to accept the current information in each field.\n To make changes enter new information.");
            Console.WriteLine("\t*******Order Number: {0}  ********", _currentOrder.LastName);
            Console.Write("\tLast Name ({0}): ", _currentOrder.LastName);
            string inputN = Console.ReadLine();
            if (inputN != "")
            {
                _currentOrder.LastName = inputN;
            }
        }

        public void ChangeState()
        {
            OrderOperations ops = new OrderOperations();
            Console.Write("State ({0}): ", _currentOrder.State);
            string inputS = Console.ReadLine();

            if (inputS.Length <= 2)
            {
                if (inputS != "") // want to change state
                {
                    _currentOrder.State = inputS;
                    _currentOrder.TaxRate = ops.MatchState(_currentOrder.State);
                }
            }
            _currentOrder.TaxRate = ops.MatchState(_currentOrder.State);
        }

        public void ChangeProduct()
        {
            OrderOperations ops = new OrderOperations();
            Console.Write("Product Type ({0}): ", _currentOrder.ProductType);
            string inputP = Console.ReadLine();
            if (inputP != "")  //want to change product
            {
                _currentOrder.ProductType = inputP;
                _currentOrder.CostSqFt = ops.ReturnCostPerSquareFoot(_currentOrder.ProductType);
            }

            _currentOrder.CostSqFt = ops.ReturnCostPerSquareFoot(_currentOrder.ProductType);
        }

        public void ChangeArea()
        {
            string inputB = "";
           Console.Write("Area {0}:", _currentOrder.Area);
            inputB = (Console.ReadLine());

            if (inputB != "")
            {
                _currentOrder.Area = int.Parse(inputB);
            }
        }

        public void ChangeMaterialCost()
        {
            OrderOperations ops = new OrderOperations();
            _currentOrder.MaterialCost = ops.MaterialCost(_currentOrder.ProductType, _currentOrder.Area);
            
        }
        public void ChangeTax()
        {
            OrderOperations ops = new OrderOperations();
            _currentOrder.Tax = ops.Tax(_currentOrder.State, _currentOrder.MaterialCost);
        }

        public void ChangeLaborPerSquareFt()
        {
            OrderOperations ops = new OrderOperations();
            _currentOrder.LaborSqFt = ops.LaborPerSquareFt(_currentOrder.ProductType);
            
        }

        public void ChangeLaborCost()
        {
            OrderOperations ops = new OrderOperations();
            _currentOrder.LaborCost = ops.LaborCost(_currentOrder.ProductType, _currentOrder.Area);
            
        }

        public void ChangeTotal()
        {
            OrderOperations ops = new OrderOperations();
            _currentOrder.Total = ops.Total(_currentOrder.MaterialCost, _currentOrder.Tax, _currentOrder.LaborCost);
           }


        public Order NewOrder()
        {
            OrderOperations ops = new OrderOperations();
            ////*************************************************************************************

            Console.Clear();
            Console.WriteLine("Here is your new order information:  ");
            DisplayOrderWorkflow dowf = new DisplayOrderWorkflow();
            Response response = new Response();
            //dowf.DisplayOrderInformation(EditedOrder);
            //Order editedOrder = dowf.DisplayOrderInformation(formattedDate, orderNumber);
            Order newEditedOrder = response.OrderInfo;
            response = ops.EditOrder(formattedDate, orderNumber, _currentOrder);
            dowf.PrintOrderInformation(response.OrderInfo);
            Console.WriteLine("Press enter for Main Menu...");
            return newEditedOrder;
        }

        //var ops = new OrderOperations();

        //var response = ops.GetOrder(file, orderNumber);
        //_currentOrder = response.OrderInfo;

        //    if (response.Success)
        //    {
        //        //_currentOrder = response.OrderInfo;
        //        PrintOrderInformation(response.OrderInfo);
        //        return _currentOrder;

        //        }
        //    else
        //    {
        //        Console.WriteLine("Error occurred!!");
        //        Console.WriteLine(response.Message);
        //        Console.WriteLine("Press enter to continue...");
        //        Console.ReadLine();
        //        return null;
        //    }


    }

}
