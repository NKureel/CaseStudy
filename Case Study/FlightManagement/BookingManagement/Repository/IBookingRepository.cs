using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingManagement.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<UserBookingTbl> GetBookingDetail();
        public void CancelBooking(string pnr);
        
        public IEnumerable<UserBookingTbl> GetBookingDetailFromPNR(string pnr);
        public IEnumerable<UserBookingTbl> GetUserHistory(string emailId);

        public  string AddBookingDetail(UserBookingTbl tbl);

        public void AddUserDetail(Person person);

        public void SaveChanges();
    }
}
