using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Abstract
{
    public class BaseParameters
    {
        public decimal InitialValue { get; set; }
        public decimal LowerBoundDiscountRate { get; set; }
        public decimal UpperBoundDiscountRate { get; set; }
        public decimal DiscountRateIncrement { get; set; }
        public decimal[] Cashflows { get; set; }
    }
}
