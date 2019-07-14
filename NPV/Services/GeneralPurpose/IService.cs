using NPV.Models.Abstract;
using NPV.Models.Domain;
using NPV.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPV.Services
{
    public interface IService
    {
        IEnumerable<CalculationVM> GetHistory();
        IEnumerable<BaseSingleNPVCalculation> ProcessCalculation(ParametersVM parameters);
    }
}