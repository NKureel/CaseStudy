using AirlineManagement.DBContext;
using Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repository
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
