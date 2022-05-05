using AirlineManagement.Repository;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
/*
 Created By: Naina Kureel
 Detail: Airline Management Web Api
*/
namespace AirlineManagement.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize(Roles =UserRoles.Admin)]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineRepository _airlineRepository;
        public AirlineController(IAirlineRepository airlineDetail)
        {
            _airlineRepository = airlineDetail;
        }


        /// <summary>
        /// Get all registered airline
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        //[Route("GetAllAirline")]
        public IActionResult Get()
        {            
                var airline = _airlineRepository.GetAirlines();
                if (airline != null)
                    return new OkObjectResult(airline);
                else
                    return new NotFoundResult();
           
        }
                       

        /// <summary>
        /// Register Airlines
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[Action]")]
        public IActionResult Register([FromBody] AirlineTbl tbl)
        {
            using (var scope = new TransactionScope())
            {
                _airlineRepository.InsertAirline(tbl);
                scope.Complete();
                return Created("api/airline/",tbl);
            }
        }


        /// <summary>
        /// Block Airlines
        /// </summary>
        /// <param name="airlineNo"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[Action]/{airlineNo}")]
        public IActionResult Block(string airlineNo)
        {
            _airlineRepository.DeleteAirline(airlineNo);
            return new OkResult();
        }
        
    }
}
