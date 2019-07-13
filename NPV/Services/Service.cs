﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPV.Models.Domain;
using NPV.Models.View;

namespace NPV.Services
{
    public class Service : IService
    {
        public IEnumerable<NPVCalculation> ProcessCalculation(ParametersVM parameters)
        {
            List<NPVCalculation> npvCalculations = new List<NPVCalculation>();
            for(
                decimal discountRate = parameters.LowerBoundDiscountRate;
                discountRate <= parameters.UpperBoundDiscountRate; 
                discountRate += parameters.DiscountRateIncrement
                )
            {
                decimal NPV = CalculateNPV(parameters.InitialValue, discountRate, parameters.Cashflows);
                npvCalculations.Add(new NPVCalculation { DiscountRate = discountRate, NPV = NPV });
            }

            return npvCalculations;
        }

        public IEnumerable<NPVCalculationsVM> GetHistory()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<decimal> CalculatePVs(decimal[] Cashflows, decimal DiscountRate)
        {
            int Year = 1;
            foreach(decimal cashflow in Cashflows)
            {
                decimal denominator = (decimal)Math.Pow((double)(1 + (DiscountRate * 0.01m)), Year);
                yield return cashflow / denominator;
                Year++;
            }
        }

        private decimal CalculateNPV(decimal InitialValue, decimal DiscountRate, decimal[] Cashflows)
        {
            IEnumerable<decimal> PVs = CalculatePVs(Cashflows, DiscountRate);
            decimal SumOfPVs = PVs.Sum();
            return -(InitialValue) + SumOfPVs;
        }
    }
}