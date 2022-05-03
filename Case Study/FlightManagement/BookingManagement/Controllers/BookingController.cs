using BookingManagement.Repository;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BookingManagement.Controllers
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
            //var allBookings = _repository.GetBookingDetail();
            var allBookings = _repository.GetBookingDetail();
            if (allBookings != null)
                return new OkObjectResult(allBookings);
            else
                return new NoContentResult();
        }

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

        [HttpDelete]
        [Route("[Action]/{pnr}")]
        public IActionResult Cancel(string pnr)
        {
            _repository.CancelBooking(pnr);
            return new OkResult();
        }

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
