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
        ICalculationService calculationService = new CalculationService();
        public NPVTest()
        {

        }

        [TestMethod]
        [DataRow(10000, 1, new double[] { 1000, 1000, 1000, 1000})]
        public void CalculateNPV_SimpleValues_Calculate
            (
                double InitialValue,
                double DiscountRate,
                double[] Cashflows
            )
        {
            //Arrange
            var x = new decimal[Cashflows.Length];
            for (int i = 0; i < Cashflows.Length; i++) x[i] = (decimal)Cashflows[i];

            //Act
            decimal NPV = calculationService.CalculateNPV(x, (decimal)DiscountRate, (decimal)InitialValue);

            //Assert
            Assert.AreEqual<decimal>(-6098.03m, NPV);
        }

        [TestMethod]
        [DataRow(10000, 1, new double[] { 1000, -1000, 1000, 1000 })]
        public void CalculateNPV_WithNegativeValues_Calculate
            (
                double InitialValue,
                double DiscountRate,
                double[] Cashflows
            )
        {
            //Arrange
            var x = new decimal[Cashflows.Length];
            for (int i = 0; i < Cashflows.Length; i++) x[i] = (decimal)Cashflows[i];

            //Act
            decimal NPV = calculationService.CalculateNPV(x, (decimal)DiscountRate, (decimal)InitialValue);

            //Assert
            Assert.AreEqual<decimal>(-8058.63m, NPV);
        }
    }
}
