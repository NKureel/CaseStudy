using BookingManagement.DBContext;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
        /// Add User Details
        /// </summary>
        /// <param name="person"></param>
        //public void AddUserDetail(UserDetailTbl person)
        //{
        //    try
        //    {
        //        var res = _Context.UserDetailTbls.Where(x => x.FirstName == person.LastName && x.LastName == person.LastName).ToList();
        //        if (res.Count != 0)
        //        {
        //            throw new Exception(person.FirstName + " " + person.LastName + " already exists");
        //        }
        //        _Context.UserDetailTbls.Add(person);
        //        SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        /// <summary>
        /// Update User Booking 
        /// Relation betwenn Person and Booking is 1 to 1
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public string AddBookingDetail(BookflightTbl tbl)
        {
            string pnr = String.Empty;
            try
            {
                Random generateRandom = new Random();
               string userpnr= generateRandom.Next(1, 100) + tbl.FlightNumber;              
                

                    if (tbl != null)
                        tbl.Pnr = userpnr;                    
                    var bookingtblres = _Context.BookflightTbls.FirstOrDefault(x => x.FlightNumber == tbl.FlightNumber && x.FirstName == tbl.FirstName && x.LastName==tbl.LastName);
                    if (bookingtblres != null)
                        throw new Exception(" User already booked ticket for flight " + tbl.FlightNumber);                    
                    {
                        _Context.BookflightTbls.Add(tbl);
                    }
                    _Context.SaveChanges();                                   
                    pnr = userpnr;
                
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
        //public FlightBookingDetails GetFlightDetail(string flightno, string seatno)
        //{

        //    try
        //    {
        //        var flight = _Context.flightDetail.Where(x => x.FlightNumber == flightno && x.seatNo == seatno).FirstOrDefault();
        //        if (flight == null)
        //            throw new Exception("Failed to book the flight");
        //        return flight;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        /// <summary>
        /// Cancel Booking
        /// </summary>
        /// <param name="pnr"></param>
        public void CancelBooking(BookflightTbl tbl)
        {
            try
            {
                _Context.BookflightTbls.Remove(tbl);
                SaveChanges();
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
        public IEnumerable<BookflightTbl> GetBookingDetailFromPNR(string pnr)
        {
            List<BookflightTbl> res = new List<BookflightTbl>();
            try
            {
                res = _Context.BookflightTbls.Where(x => x.Pnr.ToLower() == pnr.ToLower()).ToList<BookflightTbl>();
                if (res.Count == 0)
                    throw new Exception("PNR " + pnr + " is not exists. Failed to cancel the ticket");
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
                throw new Exception("Failed to save db " + ex.Message);
            }
        }

        /// <summary>
        /// Get All Booking details of Users
        /// </summary>
        /// <returns></returns>
        IEnumerable<BookflightTbl> IBookingRepository.GetBookingDetail()
        {
            try
            {
                var res = _Context.BookflightTbls.ToList();
                if (res.Count == 0)
                    throw new Exception("No booking found");
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Get History of User based upon emailId
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        IEnumerable<BookflightTbl> IBookingRepository.GetUserHistory(string emailId)
        {
            List<BookflightTbl> res = new List<BookflightTbl>();
            try
            {
                res = _Context.BookflightTbls.Where(x => x.EmailId.ToLower() == emailId.ToString().ToLower()).
                   ToList();
                if (res.Count == 0)
                    throw new Exception("No history found for emailid " + emailId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        //public string GetUserDetail(UserDetailTbl person)
        //{
        //    try
        //    {
        //        var res = _Context.UserDetailTbls.Find(person);
        //        if (res != null)
        //        {
        //            return res.PeopleId.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return "null";
        //}
    }
}
