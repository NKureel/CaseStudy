using Booking.DBContext;
using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly  BookingDbContext _Context;
        public BookingRepository(BookingDbContext context)
        {
            _Context = context;
        }
        public void AddUserBookingDetail(UserBookingTbl tbl)
        {
            _Context.bookingTbls.Add(tbl);
            SaveChanges();
            
        }

        public void CancelBooking(string pnr)
        {
            var pnrdetail = _Context.bookingTbls.Find(pnr);
            if(pnrdetail!=null)
            _Context.bookingTbls.Remove(pnrdetail);
            SaveChanges();
        }

        public IEnumerable<UserBookingTbl> GetBookingDetail()
        {
            return _Context.bookingTbls.ToList();
        }

        public IEnumerable<UserBookingTbl> GetUserHistory(string emailId)
        {
            return _Context.bookingTbls.Where(x => x.EmailId == emailId); 
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }
    }
}
