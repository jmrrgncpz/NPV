using NPV.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Domain
{
    public class Calculation : BaseParameters
    {
        public int ID { get; set; }
        public DateTime CalculationDate { get; set; }
        
        public virtual ICollection<SingleNPVCalculation> ResultSet { get; set; }
        public virtual ICollection<Cashflow> Cashflows { get; set; }
    }
}
