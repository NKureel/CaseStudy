﻿using BookingManagement.DBContext;
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
        public string AddBookingDetail(BookflightTblUsr tbl)
        {
            string pnr = String.Empty;
            string userpnr = String.Empty;
            try
            {
                foreach (var item in tbl.users)
                {
                    BookflightTbl bookflight = new BookflightTbl();
                    bookflight.EmailId = tbl.EmailId;
                    bookflight.FlightNumber = tbl.FlightNumber;
                    bookflight.Meal = tbl.Meal;
                    bookflight.Name = tbl.Name;
                    Random generateRandom = new Random();
                    int people = generateRandom.Next(1, 1000);
                    bookflight.peopleid = people;
                    bookflight.Id = generateRandom.Next(1, 1000); ;
                    //if (tbl != null)
                    //    tbl.Pnr = userpnr;
                    var bookingtblres = _Context.BookflightTbls.FirstOrDefault(x => x.FlightNumber == tbl.FlightNumber && x.peopleid == people);
                    if (bookingtblres != null)
                        throw new Exception(" User already booked ticket for flight " + tbl.FlightNumber);
                    _Context.BookflightTbls.Add(bookflight);
                    int p = generateRandom.Next(1, 1000);
                    bookflight.Pnr = "PNR" + p.ToString();
                    userpnr = userpnr + bookflight.Pnr+"  "+ item.FirstName + " " + item.LastName + "\n";
                    _Context.SaveChanges();
                    UserDetailTbl detailTbl = new UserDetailTbl();
                    detailTbl.FirstName = item.FirstName;
                    detailTbl.Gender = item.Gender;
                    detailTbl.LastName = item.LastName;
                    detailTbl.PeopleId = people;
                    detailTbl.Class = item.Class;
                    detailTbl.Age = item.Age;
                    _Context.UserDetailTbls.Add(detailTbl);
                    _Context.SaveChanges();
                    pnr = userpnr;
                }

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
        public void CancelBooking(string pnr)
        {
            try
            {
                var res=_Context.BookflightTbls.FirstOrDefault(x => x.Pnr == pnr);
                _Context.BookflightTbls.Remove(res);
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
        public IEnumerable<TicketDetail> GetBookingDetailFromPNR(string pnr)
        {
            List<TicketDetail> res = new List<TicketDetail>();
            try
            {

                var bookflights = _Context.BookflightTbls.Where(x => x.Pnr.ToLower() == pnr.ToLower()).ToList<BookflightTbl>();

                foreach (var item in bookflights)
                {
                    var inventory = _Context.InventoryTbls.Where(x => x.FlightNumber.ToLower().Trim() == item.FlightNumber.ToLower().Trim()).ToList();
                    var person = _Context.UserDetailTbls.Where(x => x.PeopleId == item.peopleid).ToList();
                    TicketDetail data = new TicketDetail();
                    data.FlightNumber = item.FlightNumber;
                    data.FirstName = person[0].FirstName;
                    data.LastName = person[0].LastName;
                    data.Meal = item.Meal;
                    data.Pnr = item.Pnr;
                    data.Emailid = item.EmailId;
                    data.ScheduleDays = inventory[0].ScheduleDays;
                    data.startDateTime = inventory[0].startDateTime;
                    data.endDateTime = inventory[0].endDateTime;
                    res.Add(data);
                }
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
        IEnumerable<TicketDetail> IBookingRepository.GetUserHistory(string emailId)
        {
            List<TicketDetail> res = new List<TicketDetail>();
            try
            {
                var flight = _Context.BookflightTbls.Where(x => x.EmailId.ToLower() == emailId.ToString().ToLower()).
                   ToList();
                for (int i = 0; i < flight.Count; i++)
                {
                    int count = 0;
                    var inventory = _Context.InventoryTbls.Where(x => x.FlightNumber.ToLower().Trim() == flight[i].FlightNumber.ToLower().Trim()).ToList();
                    var person = _Context.UserDetailTbls.Where(x => x.PeopleId == flight[i].peopleid).ToList();
                    TicketDetail data = new TicketDetail();
                    data.FlightNumber = flight[i].FlightNumber;
                    data.FirstName = person[count].FirstName;
                    data.LastName = person[count].LastName;
                    data.Meal = flight[i].Meal;
                    data.Pnr = flight[i].Pnr;
                    data.Emailid= flight[i].EmailId;
                    data.ScheduleDays = inventory[count].ScheduleDays;
                    data.startDateTime = inventory[count].startDateTime;
                    data.endDateTime = inventory[count].endDateTime;
                    res.Add(data);
                    count++;
                }
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
