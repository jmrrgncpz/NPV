﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPV.Models.Abstract
{
    public abstract class BaseSingleNPVCalculation
    {
        public decimal DiscountRate { get; set; }
        public decimal NPV { get; set; }
    }
}