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
        public IEnumerable<BaseNPVCalculation> ProcessCalculation(ParametersVM parameters)
        {
            List<BaseNPVCalculation> npvCalculations = new List<BaseNPVCalculation>();
            for (
                decimal discountRate = parameters.LowerBoundDiscountRate;
                discountRate <= parameters.UpperBoundDiscountRate;
                discountRate += parameters.DiscountRateIncrement
                )
            {
                decimal NPV = CalculateNPV(parameters.InitialValue, discountRate, parameters.Cashflows);
                npvCalculations.Add((BaseNPVCalculation)new NPVCalculation { DiscountRate = discountRate, NPV = NPV });
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
            foreach (decimal cashflow in Cashflows)
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

        public NPVCalculationsVM SaveCalculation(ParametersVM parameters, IEnumerable<BaseNPVCalculation> NPVs)
        {
            NPVCalculations savedNPVC = SaveNPVCalculations(parameters);

        }

        private NPVCalculations SaveNPVCalculations(ParametersVM parameters)
        {
            NPVCalculations npvc = new NPVCalculations
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

        private void SaveCashflows(decimal[] Cashflows, int NPVCalculationsID)
        {
            List<Cashflow> 
        }
    }
}