using Common.Models;
using InventoryManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
 Created By: Naina Kureel
 Detail: Search flight Management Web Api
*/
namespace InventoryManagement.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]   
    [AllowAnonymous]
    //[Authorize(Roles = UserRoles.User)]
    public class SearchController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public SearchController(IInventoryRepository repository)
        {
            _inventoryRepository = repository;
        }

        /// <summary>
        /// Get the flight search based upon from place and to place
        /// </summary>
        /// <param name="fromplace"></param>
        /// <param name="toplace"></param>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult Get(string fromplace,string toplace)
        {
            Response response = new Response();
            try
            {
                var flights = _inventoryRepository.GetAllFlightBasedUponPlaces(fromplace, toplace);
                if (flights.Count() != 0)
                    return new OkObjectResult(flights);
                else
                    throw new Exception("No flight exists");
            }
            catch (Exception ex)
            {
                response.Message =  ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
            }
            return new NotFoundObjectResult(Response);
        }
    }
}
