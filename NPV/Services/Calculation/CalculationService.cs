﻿using NPV.DAL;
using NPV.Models.Abstract;
using NPV.Models.Domain;
using NPV.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPV.Services
{
    public class CalculationService : ICalculationService
    {
        NPVContext dbContext = new NPVContext();
        public CalculationService() { }

        public decimal CalculateNPV(decimal[] Cashflows, decimal DiscountRate, decimal InitialValue)
        {
            IEnumerable<decimal> PVs = CalculatePVs(Cashflows, DiscountRate);
            decimal SumOfPVs = PVs.Sum();
            return Math.Round(-(InitialValue) + SumOfPVs, 2);
        }

        private IEnumerable<decimal> CalculatePVs(decimal[] Cashflows, decimal DiscountRate)
        {
            int Year = 1;
            foreach (decimal cashflow in Cashflows)
            {
                decimal denominator = (decimal)Math.Pow((double)(1 + (DiscountRate * 0.01m)), Year);
                yield return Math.Round(cashflow / denominator, 2);
                Year++;
            }
        }

        public  Calculation SaveCalculation(ParametersVM parameters, IEnumerable<BaseSingleNPVCalculation> NPVs)
        {
            Calculation npvc = new Calculation
            {
                CalculationDate = DateTime.Now,
                DiscountRateIncrement = parameters.DiscountRateIncrement,
                InitialValue = parameters.InitialValue,
                LowerBoundDiscountRate = parameters.LowerBoundDiscountRate,
                UpperBoundDiscountRate = parameters.UpperBoundDiscountRate
            };
            dbContext.NPVCalculations.Add(npvc);
            dbContext.SaveChanges();

            SaveCashflows(parameters.Cashflows, npvc.ID);
            SaveSingleNPVCalculations(NPVs, npvc.ID);

            return npvc;
        }

        private void SaveCashflows(decimal[] Cashflows, int calculationId)
        {
            foreach (decimal cashflow in Cashflows)
            {
                dbContext.Cashflow.Add(new Cashflow { Value = cashflow, CalculationID = calculationId });
            }

            dbContext.SaveChanges();
        }

        private void SaveSingleNPVCalculations(IEnumerable<BaseSingleNPVCalculation> nPVCalculations, int calculationId)
        {
            foreach (BaseSingleNPVCalculation baseNPVCalculation in nPVCalculations)
            {
                dbContext.NPVCalculation.Add(new SingleNPVCalculation
                {
                    DiscountRate = baseNPVCalculation.DiscountRate,
                    NPV = baseNPVCalculation.NPV,
                    CalculationID = calculationId
                });
            }

            dbContext.SaveChanges();
        }
    }
}