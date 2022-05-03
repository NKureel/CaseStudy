using Common.Models;
using System.Collections.Generic;

namespace BookingManagement.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<UserBookingTbl> GetBookingDetail();
        public void CancelBooking(string pnr);
        
        public IEnumerable<UserBookingTbl> GetBookingDetailFromPNR(string pnr);
        public IEnumerable<UserBookingTbl> GetUserHistory(string emailId);

        public string AddUserBookingDetail(UserBookingTbl tbl);

        public void SaveChanges();
    }
}
