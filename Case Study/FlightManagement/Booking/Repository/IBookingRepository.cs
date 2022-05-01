using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<UserBookingTbl> GetBookingDetail();
        public void CancelBooking(string pnr);

        public IEnumerable<UserBookingTbl> GetUserHistory(string emailId);

        public void AddUserBookingDetail(UserBookingTbl tbl);

        public void SaveChanges();
    }
}
