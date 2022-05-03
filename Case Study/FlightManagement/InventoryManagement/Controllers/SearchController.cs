using InventoryManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public SearchController(IInventoryRepository repository)
        {
            _inventoryRepository = repository;
        }

        [HttpGet]
        public IActionResult Get(string fromplace,string toplace)
        {
            var flights = _inventoryRepository.GetAllFlightBasedUponPlaces(fromplace, toplace);
            if (flights.Count() != 0)
                return new OkObjectResult(flights);
            else
                return new NoContentResult();
        }
    }
}
