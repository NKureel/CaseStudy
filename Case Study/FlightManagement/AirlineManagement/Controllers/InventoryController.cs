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
    [Route("api/airline/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryController(IInventoryRepository repository)
        {
            _inventoryRepository = repository;
        }

        [HttpGet]      
        public IActionResult Get()
        {
            var airline = _inventoryRepository.GetInventory();
            if (airline != null)
                return new OkObjectResult(airline);
            else
                return new NotFoundResult();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] InventoryTbl tbl)
        {
            using (var scope = new TransactionScope())
            {
                _inventoryRepository.AddInventory(tbl);
                scope.Complete();
                return Created("api/airline/inventory/", tbl);
            }
        }

    }
}
