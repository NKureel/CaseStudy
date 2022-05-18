using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingManagement.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<BookflightTbl> GetBookingDetail();
        public void CancelBooking(string pnr);
        
        public IEnumerable<TicketDetail> GetBookingDetailFromPNR(string pnr);
        public IEnumerable<TicketDetail> GetUserHistory(string emailId);
        
        public  string AddBookingDetail(BookflightTblUsr tbl);

        //public void AddUserDetail(UserDetailTbl person);

        //public string GetUserDetail(UserDetailTbl person);

        public void SaveChanges();
    }
}
