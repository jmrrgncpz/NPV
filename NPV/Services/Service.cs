using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NPV.DAL;
using NPV.Models.Abstract;
using NPV.Models.Domain;
using NPV.Models.View;

namespace NPV.Services
{
    public class Service : IService
    {
        NPVContext dbContext = new NPVContext();
        public Service()
        {

        }
        public IEnumerable<BaseSingleNPVCalculation> ProcessCalculation(ParametersVM parameters)
        {
            List<BaseSingleNPVCalculation> npvCalculations = new List<BaseSingleNPVCalculation>();
            for (
                decimal discountRate = parameters.LowerBoundDiscountRate;
                discountRate <= parameters.UpperBoundDiscountRate;
                discountRate += parameters.DiscountRateIncrement
                )
            {
                decimal NPV = CalculateNPV(parameters.InitialValue, discountRate, parameters.Cashflows);
                npvCalculations.Add((BaseSingleNPVCalculation)new SingleNPVCalculation { DiscountRate = discountRate, NPV = NPV });
            }

            return npvCalculations;
        }

        public IEnumerable<CalculationVM> GetHistory()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<decimal> CalculatePVs(decimal[] Cashflows, decimal DiscountRate)
        {
            int Year = 1;
            foreach (decimal cashflow in Cashflows)
            {
                yield return CalculatePV(cashflow, DiscountRate, Year);
                Year++;
            }
        }

        private decimal CalculatePV(decimal Cashflow, decimal DiscountRate, int Year)
        {
            decimal denominator = (decimal)Math.Pow((double)(1 + (DiscountRate * 0.01m)), Year);
            return Cashflow / denominator;
        }

        private decimal CalculateNPV(decimal InitialValue, decimal DiscountRate, decimal[] Cashflows)
        {
            IEnumerable<decimal> PVs = CalculatePVs(Cashflows, DiscountRate);
            decimal SumOfPVs = PVs.Sum();
            return -(InitialValue) + SumOfPVs;
        }

        public void SaveCalculation(ParametersVM parameters, IEnumerable<BaseSingleNPVCalculation> NPVs)
        {
            Calculation savedCalculation = SaveCalculation(parameters);
            SaveCashflows(parameters.Cashflows, savedCalculation.ID);
            SaveSingleNPVCalculations(NPVs, savedCalculation.ID);
        }

        private Calculation SaveCalculation(ParametersVM parameters)
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