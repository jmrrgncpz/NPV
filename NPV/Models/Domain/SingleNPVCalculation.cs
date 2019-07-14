using Newtonsoft.Json;
using NPV.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Domain
{
    public class SingleNPVCalculation : BaseSingleNPVCalculation
    {
        public int ID { get; set; }
        public int CalculationID { get; set; }

        [JsonIgnore]
        public virtual Calculation Calculation { get; set; }
    }
}
