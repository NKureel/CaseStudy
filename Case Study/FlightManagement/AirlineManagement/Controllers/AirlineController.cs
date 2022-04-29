using AirlineManagement.DBContext;
using AirlineManagement.Models;
using AirlineManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AirlineManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineRepository _airlineRepository;
        public AirlineController(IAirlineRepository airlineDetail)
        {
            _airlineRepository = airlineDetail;
        }

        [HttpGet]
        [Route("GetAllAirline")]
        public IActionResult Get()
        {
            var airline = _airlineRepository.GetAirlines();
            if (airline != null)
                return new OkObjectResult(airline);
            else
                return new NotFoundResult();
        }

        [HttpGet]
        public IActionResult Get(string airlineNo)
        {
            var airline=_airlineRepository.GetAirlineByNumber(airlineNo);
            if (airline != null)
                return new OkObjectResult(airline);
            else
                return new NotFoundResult();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Post([FromBody] AirlineTbl tbl)
        {
            using (var scope = new TransactionScope())
            {
                _airlineRepository.InsertAirline(tbl);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { airlineNo = tbl.AirlineNo }, tbl);
            }
        }

        [HttpDelete]        
        public IActionResult Delete(string airlineNo)
        {
            _airlineRepository.DeleteAirline(airlineNo);
            return new OkResult();
        }

        [HttpPut]
        public IActionResult Put([FromBody] AirlineTbl tbl)
        {
            if (tbl != null)
            {
                using (var scope = new TransactionScope())
                {
                    _airlineRepository.UpdateAirline(tbl);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
    }
}
