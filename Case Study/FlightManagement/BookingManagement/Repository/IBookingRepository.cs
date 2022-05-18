using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingManagement.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<BookflightTbl> GetBookingDetail();
        public void CancelBooking(BookflightTbl tbl);
        
        public IEnumerable<BookflightTbl> GetBookingDetailFromPNR(string pnr);
        public IEnumerable<BookflightTbl> GetUserHistory(string emailId);
        
        public  string AddBookingDetail(BookflightTbl tbl);

        //public void AddUserDetail(UserDetailTbl person);

        //public string GetUserDetail(UserDetailTbl person);

        public void SaveChanges();
    }
}
