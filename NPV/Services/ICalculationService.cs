using NPV.Models.Abstract;
using NPV.Models.Domain;
using NPV.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPV.Services
{
    public interface ICalculationService
    {
        decimal CalculateNPV(decimal[] Cashflows, decimal DiscountRate, decimal InitialValue);
        Calculation SaveCalculation(ParametersVM parameters);
        void SaveCashflows(decimal[] Cashflows, int calculationId);
        void SaveSingleNPVCalculations(IEnumerable<BaseSingleNPVCalculation> nPVCalculations, int calculationId);
    }
}