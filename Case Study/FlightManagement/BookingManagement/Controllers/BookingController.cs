using BookingManagement.Repository;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
/*
 Created By: Naina Kureel
 Detail: Booking Management Web Api
*/
namespace BookingManagement.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize(Roles = UserRoles.User)]
    public class BookingController : ControllerBase
    {
        IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all Booking Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            //var allBookings = _repository.GetBookingDetail();
            var allBookings = _repository.GetBookingDetail();
            if (allBookings != null)
                return new OkObjectResult(allBookings);
            else
                return new NoContentResult();
        }

        /// <summary>
        /// Booking Details for user
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("{flightid}")]
        public string Post([FromBody] UserBookingTbl userDetail)
        {
            using(var scope=new TransactionScope())
            {
                var res=_repository.AddUserBookingDetail(userDetail);
                scope.Complete();
                return res;
            }
        }

        /// <summary>
        /// Get history based upon user's emailid
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[Action]/{emailId}")]
        public IActionResult History(string emailId)
        {
            var history = _repository.GetUserHistory(emailId);
            if (history != null)
                return new OkObjectResult(history);
            else
                return new NoContentResult();

        }


        /// <summary>
        /// Cancel booking based upon pnr
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[Action]/{pnr}")]
        public IActionResult Cancel(string pnr)
        {
            _repository.CancelBooking(pnr);
            return new OkResult();
        }



        /// <summary>
        /// Get Ticket detail based upon pnr
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[Action]/{pnr}")]
        public IActionResult Ticket(string pnr)
        {
            var result = _repository.GetBookingDetailFromPNR(pnr);
            if (result != null)
                return new OkObjectResult(result);
            else
                return new NotFoundResult();
        }
    }
}
