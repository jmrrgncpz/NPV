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
            var groupedCashFlows= GetAllCashflows().GroupBy(x => x.CalculationID);
            var groupedSingleNPVCalculations = GetAllSingleNPVCalculations().GroupBy(x => x.CalculationID);

            return from calculation in calculations
                   join cf in groupedCashFlows on calculation.ID equals cf.Key
                   join npvc in groupedSingleNPVCalculations on calculation.ID equals npvc.Key
                   select new CalculationVM
                   {
                       CalculationDate = calculation.CalculationDate,
                       Cashflows = cf.ToList(),
                       DiscountRateIncrement = calculation.DiscountRateIncrement,
                       ID = calculation.ID,
                       InitialValue = calculation.InitialValue,
                       LowerBoundDiscountRate = calculation.LowerBoundDiscountRate,
                       ResultSet = npvc.ToList(),
                       UpperBoundDiscountRate = calculation.UpperBoundDiscountRate
                   };
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