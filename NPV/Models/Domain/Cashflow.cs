using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Domain
{
    public class Cashflow
    {
        public int ID { get; set; }
        public int ParametersID { get; set; }
        public decimal Value { get; set; }
    }
}
