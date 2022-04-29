using AirlineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineManagement.Repository
{
    public interface IInventoryRepository
    {
        public void AddInventory(InventoryTbl tbl);
        public IEnumerable<InventoryTbl> GetInventory();
    }
}
