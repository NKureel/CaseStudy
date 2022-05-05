using Common.Models;
using InventoryManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

/*
 Created By: Naina Kureel
 Detail: Inventory Management Web Api
*/
namespace InventoryManagement.Controllers
{    
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/airline/[controller]")]
    [ApiController]
    [Authorize(Roles =UserRoles.Admin)]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryController(IInventoryRepository repository)
        {
            _inventoryRepository = repository;
        }


        /// <summary>
        /// Get the Inventoyr detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]      
        public IActionResult Get()
        {
            var airline = _inventoryRepository.GetInventory();
            if (airline!= null)
                return new OkObjectResult(airline);
            else
                return new NotFoundResult();
        }


        /// <summary>
        /// Add Inventory for airlines
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
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
