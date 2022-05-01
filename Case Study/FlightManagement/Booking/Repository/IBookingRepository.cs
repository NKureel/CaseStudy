using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Repository
{
    interface IBookingRepository
    {
      //  public IEnumerable<UserBookingTbl> GetUserBookingDetail(string fligntNo);
        public void CancelBooking(string pnr);

        public IEnumerable<UserBookingTbl> GetUserHistory(string emailId);

        public void AddUserBookingDetail(UserBookingTbl tbl);
    }
}
