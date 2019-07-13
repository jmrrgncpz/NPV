using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Domain
{
    public class NPVCalculation
    {
        public int ID { get; set; }
        public int NPVCalculationsID { get; set; }
        public decimal NPV { get; set; }
        public decimal DiscountRate { get; set; }

        public virtual NPVCalculations NPVCalculations { get; set; }
    }
}
