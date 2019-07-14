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
    [RoutePrefix("/api")]
    public class APIController : ApiController
    {
        IService service = new Service();
        IHistoryService historyService = new HistoryService();
        public APIController()
        {

        }

        [Route("calculate")]
        public IHttpActionResult Calculate(ParametersVM parameters)
        {
            IEnumerable<BaseSingleNPVCalculation> npvCalculations = service.ProcessCalculation(parameters);
            return Ok(npvCalculations);
        }

        [Route("history")]
        public IHttpActionResult GetHistory()
        {
            var history = historyService.GetAll();
            return Ok(history);
        }
    }
}
