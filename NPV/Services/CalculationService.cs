using NPV.DAL;
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
            return -(InitialValue) + SumOfPVs;
        }

        private IEnumerable<decimal> CalculatePVs(decimal[] Cashflows, decimal DiscountRate)
        {
            int Year = 1;
            foreach (decimal cashflow in Cashflows)
            {
                decimal denominator = (decimal)Math.Pow((double)(1 + (DiscountRate * 0.01m)), Year);
                yield return cashflow / denominator;
                Year++;
            }
        }

        public Calculation SaveCalculation(ParametersVM parameters)
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

            return npvc;
        }

        public void SaveCashflows(decimal[] Cashflows, int calculationId)
        {
            foreach (decimal cashflow in Cashflows)
            {
                dbContext.Cashflow.Add(new Cashflow { Value = cashflow, CalculationID = calculationId });
            }

            dbContext.SaveChanges();
        }

        public void SaveSingleNPVCalculations(IEnumerable<BaseSingleNPVCalculation> nPVCalculations, int calculationId)
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