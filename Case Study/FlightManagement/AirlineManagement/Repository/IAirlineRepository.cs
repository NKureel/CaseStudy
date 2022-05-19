using Common.Models;
using System.Collections.Generic;
/*
 Created By: Naina Kureel
 Detail: Airline Management Interface
*/
namespace AirlineManagement.Repository
{
    public interface IAirlineRepository
    {
        IEnumerable<AirlineTbl> GetAirlines();
        public void InsertAirline(AirlineTbl tbl);

        public void DeleteAirline(string airlineNo);

        public AirlineTbl GetAirlineByNumber(string airlineNo);

        public void UpdateAirline(AirlineTbl tbl);

        public void Save();       
    }
}
