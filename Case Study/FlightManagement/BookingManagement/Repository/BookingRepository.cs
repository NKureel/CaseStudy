using BookingManagement.DBContext;
using Common.Models;
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
            return _Context.bookingTbls.Where(x => x.EmailId == emailId);
        }
    }
}
