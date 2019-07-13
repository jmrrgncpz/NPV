using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPV.Models.Domain;
using NPV.Models.View;

namespace NPV.Services
{
    public class Service : IService
    {
        public NPVCalculationsVM Calculate(ParametersVM parameters)
        {
            throw new NotImplementedException();
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

        private NPVCalculation CalculateNPV(decimal InitialValue, decimal DiscountRate, decimal[] Cashflows)
        {
            IEnumerable<decimal> PVs = CalculatePVs(Cashflows, DiscountRate);
        }
    }
}