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
        ICalculationService calculationService = new CalculationService();
        NPVContext dbContext = new NPVContext();

        public Service() { }
        public IEnumerable<BaseSingleNPVCalculation> ProcessCalculation(ParametersVM parameters)
        {
            List<BaseSingleNPVCalculation> npvCalculations = new List<BaseSingleNPVCalculation>();
            for (
                decimal discountRate = parameters.LowerBoundDiscountRate;
                discountRate <= parameters.UpperBoundDiscountRate;
                discountRate += parameters.DiscountRateIncrement
                )
            {
                decimal NPV = calculationService.CalculateNPV(parameters.Cashflows, discountRate, parameters.InitialValue);
                npvCalculations.Add((BaseSingleNPVCalculation)new SingleNPVCalculation { DiscountRate = discountRate, NPV = NPV });
            }

            calculationService.SaveCalculation(parameters, npvCalculations);

            return npvCalculations;
        }

        public IEnumerable<CalculationVM> GetHistory()
        {
            throw new NotImplementedException();
        }
    }
}