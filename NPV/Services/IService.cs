using NPV.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPV.Services
{
    public interface IService
    {
        IEnumerable<NPVCalculationsVM> GetHistory();
        NPVCalculationsVM Calculate(ParametersVM parameters);
    }
}