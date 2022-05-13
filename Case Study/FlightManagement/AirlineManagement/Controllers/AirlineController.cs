using AirlineManagement.Repository;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult Get()
        {
            Response response = new Response();
            try
            {
                var airline = _airlineRepository.GetAirlines();                     
                if (airline != null)
                {                   
                    return new OkObjectResult(airline);
                }
                throw new Exception("Failed to get all airlines");   
            }
            catch(Exception ex) 
            {
                response.Message = ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
            }
            return new NotFoundObjectResult(response);
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
            Response response = new Response();
            try
            {
                using (var scope = new TransactionScope())
                {
                    _airlineRepository.InsertAirline(tbl);
                    scope.Complete();
                    response.Message = "Successfully register airline";
                    response.Status = "Success";
                    response.StatusCode = StatusCodes.Status200OK.ToString();
                    //return Created("api/airline/", tbl);
                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "Failed to register airlines "+ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
            }
            return new NotFoundObjectResult(response);
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
            Response response = new Response();
            try
            {
                _airlineRepository.DeleteAirline(airlineNo);
                response.Message = "Deleted Successfully";
                response.Status = "Success";
                response.StatusCode = StatusCodes.Status200OK.ToString();
               
            }
            catch (Exception ex)
            {
                response.Message = "Deletion failed "+ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
            }
            return new OkObjectResult(response);
        }
        
    }
}
