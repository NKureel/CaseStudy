using BookingManagement.DBContext;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
/*
 Created By: Naina Kureel
 Detail: Booking Management Repository
*/
namespace BookingManagement.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _Context;
        public BookingRepository(BookingDbContext context)
        {
            _Context = context;
        }

        /// <summary>
        /// Update User Booking
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public string AddUserBookingDetail(UserBookingTbl tbl)
        {
            string pnr = String.Empty;
            try
            {
                var detail = GetFlightDetail(tbl.FlightNumber, tbl.SeatNo);
                if (detail.Count() == 0)
                {
                    throw new Exception("Failed to book the flight");
                }
                foreach (var flightdetail in detail)
                {
                    if (flightdetail.status == SeatStatus.Booked)
                        throw new Exception("SeatNo " + tbl.SeatNo + " is already occupied by another user. Please select different seat");
                }
                Random generateRandom = new Random();
                if (tbl != null)
                    tbl.Pnr = generateRandom.Next(1, 100) + tbl.FlightNumber;
                var res = _Context.person.Find(tbl.peopleId.Name);
                if (res != null)
                {
                    tbl.peopleId = res;
                }
                _Context.bookingTbls.Add(tbl);
                SaveChanges();
                pnr = tbl.Pnr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pnr;
        }
        

        /// <summary>
        /// Get Flight details
        /// </summary>
        /// <param name="flightno"></param>
        /// <param name="seatno"></param>
        /// <returns></returns>
        public IEnumerable<FlightBookingDetails> GetFlightDetail(string flightno,string seatno)
        {
            
            try {
                var flight = _Context.flightDetail.Where(x => x.FlightNumber == flightno && x.seatNo == seatno).ToList();
                if (flight.Count == 0)                 
                    throw new Exception("Failed to book the flight");
                return flight;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        

        /// <summary>
        /// Cancel Booking
        /// </summary>
        /// <param name="pnr"></param>
        public void CancelBooking(string pnr)
        {
            try
            {
                var pnrdetail = _Context.bookingTbls.Where(x => x.Pnr.ToLower() == pnr.ToLower()).ToList(); ;
                if (pnrdetail != null)
                {
                    foreach (var cancel in pnrdetail)
                    {
                        //var persondetail = _Context.person.Where(x => x.PeopleId == cancel.id).ToList();
                        //foreach(var person in persondetail)
                        //_Context.person.Remove(person);
                        _Context.bookingTbls.Remove(cancel);
                        SaveChanges();
                    }
                }
                throw new Exception("PNR " + pnr + " is not exists. Failed to cancel the ticket");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
       
        /// <summary>
        /// Get User Booking Detail based upon PNR
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        public IEnumerable<UserBookingTbl> GetBookingDetailFromPNR(string pnr)
        {
            List<UserBookingTbl> res = new List<UserBookingTbl>();
            try
            {
                 res= _Context.bookingTbls.Where(x => x.Pnr.ToLower() == pnr.ToLower()).ToList<UserBookingTbl>();
                if (res.Count == 0)                  
                    throw new Exception("Failed to get the booking detail for PNR " + pnr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;            
        }

        /// <summary>
        /// Save Changes in db
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                _Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to save db "+ex.Message);
            }
        }

        /// <summary>
        /// Get All Booking details of Users
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserBookingTbl> IBookingRepository.GetBookingDetail()
        {
            List<UserBookingTbl> res = new List<UserBookingTbl>();
            try
            {
                res= _Context.bookingTbls.ToList();
                if (res.Count == 0)
                    throw new Exception("No booking found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }
        
        /// <summary>
        /// Get History of User based upon emailId
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        IEnumerable<UserBookingTbl> IBookingRepository.GetUserHistory(string emailId)
        {
            List<UserBookingTbl> res = new List<UserBookingTbl>();
            try
            {
                res = _Context.bookingTbls.Where(x => x.EmailId.ToLower() == emailId.ToString().ToLower()).
                   ToList();
                if (res.Count == 0)
                    throw new Exception("No history found for emailid "+emailId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }
    }
}
