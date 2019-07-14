using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPV.DAL;
using NPV.Models.Domain;
using NPV.Models.View;

namespace NPV.Services
{
    public class HistoryService : IHistoryService
    {
        NPVContext dbContext = new NPVContext();
        public IEnumerable<CalculationVM> GetAll()
        {
            var calculations = GetAllCalculations();

            var y = from calculation in calculations
                    orderby calculation.CalculationDate descending
                   select new CalculationVM
                   {
                       CalculationDate = calculation.CalculationDate,
                       Cashflows = calculation.Cashflows,
                       DiscountRateIncrement = calculation.DiscountRateIncrement,
                       ID = calculation.ID,
                       InitialValue = calculation.InitialValue,
                       LowerBoundDiscountRate = calculation.LowerBoundDiscountRate,
                       ResultSet = calculation.ResultSet,
                       UpperBoundDiscountRate = calculation.UpperBoundDiscountRate
                   };

            return y;
        }

        private IEnumerable<Cashflow> GetAllCashflows()
        {
            return dbContext.Cashflow.ToList();
        }

        private IEnumerable<Calculation> GetAllCalculations()
        {
            return dbContext.NPVCalculations.ToList();
        }

        private IEnumerable<SingleNPVCalculation> GetAllSingleNPVCalculations()
        {
            return dbContext.NPVCalculation.ToList();
        }
    }
}