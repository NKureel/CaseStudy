
using Common.Models;
using InventoryManagement.DBContext;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _inventoryContext;
        public InventoryRepository(InventoryDbContext context)
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

        public IEnumerable<InventoryTbl> GetAllFlightBasedUponPlaces(string fromplace, string toplace) 
        {
            return _inventoryContext.inventoryTbls.Where(x => x.ToPlace.ToLower() == toplace.ToLower() && x.FromPlace.ToLower()== fromplace.ToLower()).ToList();
        }
        
    }
}
