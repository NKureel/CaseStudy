using AirlineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
