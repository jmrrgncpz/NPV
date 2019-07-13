using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Domain
{
    public class NPVCalculations
    {
        public int ID { get; set; }
        public DateTime CalculationDate { get; set; }

        public virtual Parameters Parameters { get; set; }
        public virtual ICollection<NPVCalculation> ResultSet { get; set; }
    }
}
