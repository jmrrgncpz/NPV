using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPV.Models.Abstract
{
    public abstract class BaseCashflow
    {
        public int NPVCalculationsID { get; set; }
        public decimal Value { get; set; }
    }
}