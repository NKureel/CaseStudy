using AirlineManagement.DBContext;
using AirlineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineManagement.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AirlineDbContext _inventoryContext;
        public InventoryRepository(AirlineDbContext context)
        {
            _inventoryContext = context;
        }
        public IEnumerable<InventoryTbl> GetInventory()
        {
            return _inventoryContext.inventoryTbls.ToList();
        }

        public void AddInventory(InventoryTbl tbl)
        {
            _inventoryContext.inventoryTbls.Add(tbl);
            Save();
        }
        public void Save()
        {
            _inventoryContext.SaveChanges();
        }
        
    }
}
