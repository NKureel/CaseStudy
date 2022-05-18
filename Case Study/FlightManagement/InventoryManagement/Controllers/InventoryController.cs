using Common.Models;
using InventoryManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            Response response = new Response();
            try
            {
                var airline = _inventoryRepository.GetInventory();
                if (airline == null)
                    throw new Exception("No flight exists");
                else
                    return new OkObjectResult(airline);
            }
            catch (Exception ex)
            {
                if (String.Equals(ex.Message, "No Inventory exists", StringComparison.OrdinalIgnoreCase) || String.Equals(ex.Message, "No flight exists", StringComparison.OrdinalIgnoreCase))
                {
                    response.Message = ex.Message;
                    response.Status = "Success";
                    response.StatusCode = StatusCodes.Status200OK.ToString();
                    return new OkObjectResult(ex.Message);
                }
                response.Message = ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
            }
            return new NotFoundObjectResult(response);
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
            Response response = new Response();
            try
            {
                using (var scope = new TransactionScope())
                {
                    _inventoryRepository.AddInventory(tbl);
                    scope.Complete();
                    response.Message = "Successfully add inevntory ";
                    response.Status = "Success";
                    response.StatusCode = StatusCodes.Status200OK.ToString();
                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "Failed to add " + ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
            }
            return new NotFoundObjectResult(response);
        }

        

    }
}
