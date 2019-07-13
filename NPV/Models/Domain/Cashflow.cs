﻿using NPV.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.Models.Domain
{
    public class Cashflow : BaseCashflow
    {
        public int ID { get; set; }

        public virtual NPVCalculations NPVCalculations { get; set; }
    }
}
