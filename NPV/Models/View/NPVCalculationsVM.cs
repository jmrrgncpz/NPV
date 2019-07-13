using NPV.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.View
{
    public class NPVCalculationsVM
    {
        public int ID { get; set; }
        public Parameters Parameters { get; set; }
        public IEnumerable<NPVCalculation> ResultSet { get; set; }
    }
}
