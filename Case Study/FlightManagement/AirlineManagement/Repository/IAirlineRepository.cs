using Common.Models;
using System.Collections.Generic;

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
        //public void AddInventory(InventoryTbl tbl);
        //public IEnumerable<InventoryTbl> GetInventory();
    }
}
