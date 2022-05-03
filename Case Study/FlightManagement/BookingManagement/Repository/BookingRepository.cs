using BookingManagement.DBContext;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagement.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly  BookingDbContext _Context;
        public BookingRepository(BookingDbContext context)
        {
            _Context = context;
        }
        

        public string AddUserBookingDetail(UserBookingTbl tbl)
        {
            Random generateRandom = new Random();
            if (tbl != null)
                tbl.Pnr = generateRandom.Next(1,100) + tbl.FlightNumber;
            _Context.bookingTbls.Add(tbl);
            SaveChanges();
            return tbl.Pnr;
        }

        public void CancelBooking(string pnr)
        {
            var pnrdetail = _Context.bookingTbls.Where(x => x.Pnr.ToLower() == pnr.ToLower()).ToList(); ;
            if (pnrdetail != null)
            {
                foreach (var cancel in pnrdetail)
                {
                    var persondetail = _Context.person.Where(x => x.PeopleId == cancel.id).ToList();
                    foreach(var person in persondetail)
                    _Context.person.Remove(person);
                    _Context.bookingTbls.Remove(cancel);

                    SaveChanges();
                }
            }
            
        }
       
        public IEnumerable<UserBookingTbl> GetBookingDetailFromPNR(string pnr)
        {
            return _Context.bookingTbls.Where(x => x.Pnr.ToLower()==pnr.ToLower()).ToList<UserBookingTbl>();
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        IEnumerable<UserBookingTbl> IBookingRepository.GetBookingDetail()
        {
            return _Context.bookingTbls.ToList();
        }

        IEnumerable<UserBookingTbl> IBookingRepository.GetUserHistory(string emailId)
        {

            
           var res = _Context.bookingTbls.Where(x => x.EmailId.ToLower() == emailId.ToString().ToLower()).
               ToList();
           
            return res;
        }
    }
}
