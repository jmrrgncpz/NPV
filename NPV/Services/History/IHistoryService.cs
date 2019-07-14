using NPV.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPV.Services
{
    public interface IHistoryService
    {
        IEnumerable<CalculationVM> GetAll();
    }
}
