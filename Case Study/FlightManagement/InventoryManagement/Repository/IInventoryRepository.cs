using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Repository
{
    public interface IInventoryRepository
    {
        public void AddInventory(InventoryTbl tbl);
        public IEnumerable<InventoryTbl> GetInventory();
        public IEnumerable<InventoryTbl> GetAllFlightBasedUponPlaces(string fromplace, string toplace);
    }
}
