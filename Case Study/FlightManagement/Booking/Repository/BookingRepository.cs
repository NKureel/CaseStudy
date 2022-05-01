using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Repository
{
    public class BookingRepository : IBookingRepository
    {
        public void AddUserBookingDetail(UserBookingTbl tbl)
        {
            throw new NotImplementedException();
        }

        public void CancelBooking(string pnr)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserBookingTbl> GetUserHistory(string emailId)
        {
            throw new NotImplementedException();
        }
    }
}
