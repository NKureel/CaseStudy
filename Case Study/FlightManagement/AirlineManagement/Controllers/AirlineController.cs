using AirlineManagement.Repository;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace AirlineManagement.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
 
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineRepository _airlineRepository;
        public AirlineController(IAirlineRepository airlineDetail)
        {
            _airlineRepository = airlineDetail;
        }

        [HttpGet]
        //[Authorize]
        //[Route("GetAllAirline")]
        public IActionResult Get()
        {
            var airline = _airlineRepository.GetAirlines();
            if (airline!= null)
                return new OkObjectResult(airline);
            else
                return new NotFoundResult();
        }
                       


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

        [HttpDelete]
        [Route("[Action]/{airlineNo}")]
        public IActionResult Block(string airlineNo)
        {
            _airlineRepository.DeleteAirline(airlineNo);
            return new OkResult();
        }

        //[HttpPut]
        //[Route("[Action]")]
        //public IActionResult Update([FromBody] AirlineTbl tbl)
        //{
        //    if (tbl != null)
        //    {
        //        using (var scope = new TransactionScope())
        //        {
        //            _airlineRepository.UpdateAirline(tbl);
        //            scope.Complete();
        //            return new OkResult();
        //        }
        //    }
        //    return new NoContentResult();
        //}

        //[HttpGet]
        //[Route("[Action]/get")]
        //public IActionResult Inventory()
        //{
        //    var airline = _airlineRepository.GetInventory();
        //    if (airline != null)
        //        return new OkObjectResult(airline);
        //    else
        //        return new NotFoundResult();
        //}

        //[HttpPost]
        //[Route("[Action]/add")]
        //public IActionResult Inventory([FromBody] InventoryTbl tbl)
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        _airlineRepository.AddInventory(tbl);
        //        scope.Complete();
        //        return Created("api/airline/inventory/", tbl);
        //    }
        //}
    }
}
