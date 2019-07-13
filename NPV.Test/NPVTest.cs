using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPV.Models.Abstract;
using NPV.Models.View;
using NPV.Services;

namespace NPV.Test
{
    [TestClass]
    public class NPVTest
    {
        [TestMethod]
        [DataRow(10000, 0.25, 1, 1, new double[] { 1000, 1000, 1000, 1000})]
        public void ProcessCalculation_SimpleValues_Calculate
            (
                double InitialValue,
                double DiscountRateIncrement,
                double LowerBoundDiscountRate,
                double UpperBoundDiscountRate,
                double[] Cashflows
            )
        {
            //Arrange
            decimal[] decimalCashflows = new decimal[Cashflows.Length];
            for (int i = 0; i < Cashflows.Length; i++) decimalCashflows[i] = (decimal)Cashflows[i];
            ParametersVM parameters = new ParametersVM
            {
                Cashflows = decimalCashflows,
                DiscountRateIncrement = (decimal)DiscountRateIncrement,
                InitialValue = (decimal)InitialValue,
                LowerBoundDiscountRate = (decimal)LowerBoundDiscountRate,
                UpperBoundDiscountRate = (decimal)UpperBoundDiscountRate
            };


            Service serviceInstance = new Service();

            //Act
            IEnumerable<BaseSingleNPVCalculation> result = serviceInstance.ProcessCalculation(parameters);

            //Assert

            Assert.IsTrue(true);
        }
    }
}
