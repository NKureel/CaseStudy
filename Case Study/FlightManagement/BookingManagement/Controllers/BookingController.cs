using BookingManagement.Repository;
using Common.Models;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IBookingRepository _repository;
        private ITopicProducer<UserBookingTbl> _topicProducer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="topic"></param>
        public BookingController(IBookingRepository repository, ITopicProducer<UserBookingTbl> topic)
        {
            _repository = repository;
            _topicProducer = topic;

        }

        /// <summary>
        /// Get all Booking Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllBooking")]
        public IActionResult Get()
        {
            Response response = new Response();
            try
            {
                var allBookings = _repository.GetBookingDetail();
                if (allBookings != null)
                    return new OkObjectResult(allBookings);
                else
                    throw new Exception("No record found");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status404NotFound.ToString();
            }
            return new NotFoundObjectResult(response);

        }

        /// <summary>
        /// Add User Details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUserDetail")]
        public IActionResult AddUserDetail([FromBody] Person user)
        {
            Response response = new Response();
            try
            {
                _repository.AddUserDetail(user);
                response.Message = "Successfully add user detail";
                response.StatusCode = StatusCodes.Status200OK.ToString();
                response.Status = "Success";
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                response.Status = "Error";
            }
            return new NotFoundObjectResult(response);
        }

        /// <summary>
        /// Booking Details for user
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{flightid}")]
        public async Task<IActionResult> Post([FromBody] UserBookingTbl bookingDetail)
        {
            Response response = new Response();            
            var list = new List<Tuple<UserBookingTbl, string>>();
            try
            {
                using (var scope = new TransactionScope())
                {
                    //list.Add(new Tuple<UserBookingTbl, string>(bookingDetail, "Add"));
                    //await _topicProducer.Produce(list);
                    var res = _repository.AddBookingDetail(bookingDetail);
                    scope.Complete();
                    await _topicProducer.Produce(new UserBookingTbl { FlightNumber = bookingDetail.FlightNumber, SeatNo = bookingDetail.SeatNo, SeatClass = bookingDetail.SeatClass });                    
                    response.Message = "PNR " + res;
                    response.StatusCode = StatusCodes.Status200OK.ToString();
                    response.Status = "Success";
                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                response.Status = "Error";
            }
            return new NotFoundObjectResult(response);
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
            Response response = new Response();
            try
            {
                var history = _repository.GetUserHistory(emailId);
                if (history != null)
                    return new OkObjectResult(history);
                else
                    throw new Exception("No history found");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                response.Status = "Error";
            }
            return new NotFoundObjectResult(response);
        }


        /// <summary>
        /// Cancel booking based upon pnr
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[Action]/{pnr}")]
        public async Task<IActionResult> Cancel(string pnr)
        {
            Response response = new Response();
            try
            {
                using (var scope = new TransactionScope())
                {
                    var res = _repository.GetBookingDetailFromPNR(pnr);
                    //var list = new List<Tuple<UserBookingTbl, string>>();
                    foreach (var flight in res)
                    {
                        //list.Add(new Tuple<UserBookingTbl, string>(flight, "Delete"));
                        //await _topicProducer.Produce(list);
                        //await _topicProducer.Produce( new UserBookingTbl { FlightNumber = flight.FlightNumber, SeatNo = flight.SeatNo, SeatClass = flight.SeatClass });                        
                        _repository.CancelBooking(flight);
                        scope.Complete();
                        response.Message = "Successfully deleted";
                        response.StatusCode = StatusCodes.Status200OK.ToString();
                        response.Status = "Success";
                        return new OkObjectResult(response);
                    }
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                response.Status = "Error";
            }
            return new NotFoundObjectResult(response);
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
            Response response = new Response();
            try
            {
                var result = _repository.GetBookingDetailFromPNR(pnr);
                if (result != null)
                {
                    return new OkObjectResult(result);
                }
                else
                    throw new Exception("Failed to get ticket based upon PNR " + pnr);
            }
            catch (Exception ex) { response.Message = ex.Message; response.StatusCode = StatusCodes.Status500InternalServerError.ToString(); response.Status = "Error"; }
            return new NotFoundObjectResult(response);
        }
    }
}
