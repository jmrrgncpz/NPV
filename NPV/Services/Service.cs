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

        private decimal CalculatePV(decimal Cashflow, decimal DiscountRate, int Year)
        {
            decimal denominator = (decimal)Math.Pow((double)(1 + (DiscountRate * 0.01m)), Year);
            return Cashflow / denominator;
        }

        private NPVCalculation CalculateNPV()
        {

        }
    }
}