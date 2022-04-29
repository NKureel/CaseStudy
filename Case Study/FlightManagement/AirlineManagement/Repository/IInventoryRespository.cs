using AirlineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineManagement.Repository
{
    public interface IInventoryRespository
    {
        public void AddInventory(InventoryTbl tbl);
        public IEnumerable<InventoryTbl> GetInventory();
    }
}
