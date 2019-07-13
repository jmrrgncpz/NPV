using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Domain
{
    public class Cashflow
    {
        public int ID { get; set; }
        public int ParametersID { get; set; }
        public decimal Value { get; set; }

        [ForeignKey("ParametersID")]
        public virtual Parameters Parameters { get; set; }
    }
}
