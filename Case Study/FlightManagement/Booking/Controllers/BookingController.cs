using Booking.Models;
using Booking.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Booking.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]    
    public class BookingController : ControllerBase
    {
        IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allBookings = _repository.GetBookingDetail();
            if (allBookings != null)
                return new OkObjectResult(allBookings);
            else
                return new NoContentResult();
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserBookingTbl userDetail)
        {
            using(var scope=new TransactionScope())
            {
                _repository.AddUserBookingDetail(userDetail);
                scope.Complete();
                return new OkResult();
            }
        }

        [HttpGet]
        [Route("[Action]/{emailid}")]
        public IActionResult History(string emailId)
        {
            var history = _repository.GetUserHistory(emailId);
            if (history != null)
                return new OkObjectResult(history);
            else
                return new NoContentResult();

        }

        [HttpDelete]
        [Route("[Action]/{pnr}")]
        public IActionResult Cancel(string pnr)
        {
            _repository.CancelBooking(pnr);
            return new OkResult();
        }
    }
}
