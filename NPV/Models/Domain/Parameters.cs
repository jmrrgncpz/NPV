using NPV.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Domain
{
    public class Parameters : BaseParameters
    {
        public int ID { get; set; }
        public int NPVCalculationsID { get; set; }
    }
}
