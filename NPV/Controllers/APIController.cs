using NPV.Models.Abstract;
using NPV.Models.Domain;
using NPV.Models.View;
using NPV.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NPV.Controllers
{
    [RoutePrefix("api")]
    public class APIController : ApiController
    {
        IService service = new Service();
        IHistoryService historyService = new HistoryService();
        public APIController()
        {

        }

        [HttpPost]
        [Route("calculate")]
        public IHttpActionResult Calculate(ParametersVM parameters)
        {
            IEnumerable<SingleNPVCalculation> npvCalculations = service.ProcessCalculation(parameters);
            IEnumerable<Cashflow> cashFlows = parameters.Cashflows.Select(y => new Cashflow { Value = y });

            var x = new Calculation
            {
                CalculationDate = DateTime.Now,
                Cashflows = cashFlows.ToArray(),
                DiscountRateIncrement = parameters.DiscountRateIncrement,
                InitialValue = parameters.InitialValue,
                LowerBoundDiscountRate = parameters.LowerBoundDiscountRate,
                UpperBoundDiscountRate = parameters.UpperBoundDiscountRate,
                ResultSet = npvCalculations.ToArray()
            };

            return Ok(x);
        }

        [HttpGet]
        [Route("history")]
        public IHttpActionResult GetHistory()
        {
            var history = historyService.GetAll();
            return Ok(history);
        }
    }
}
