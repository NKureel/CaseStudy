
using Common.Models;
using InventoryManagement.DBContext;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace InventoryManagement.Repository
{
    public class InventoryRepository : IInventoryRepository, IConsumer<BookflightTblUsr>
    {
        private readonly InventoryDbContext _inventoryContext;
        public InventoryRepository(InventoryDbContext context)
        {
            _inventoryContext = context;
        }
       
        public  Task Consume(ConsumeContext<BookflightTblUsr> context)
        {
            try
            {                
                string flightno = context.Message.FlightNumber;             
                var tbl = _inventoryContext.inventoryTbls.Find(flightno);
                tbl.NoOfRows -= context.Message.count;
                    _inventoryContext.Entry(tbl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                     this._inventoryContext.SaveChangesAsync();
                    return Task.CompletedTask;
                                  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Get all Inventory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InventoryTbl> GetInventory()
        {
            Response response = new Response();
            try
            {
                var res = _inventoryContext.inventoryTbls.ToList();
                if (res.Count == 0)
                    throw new Exception("No Inventory exists");
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddInventory(InventoryTbl tbl)
        {
            try
            {
                var res = _inventoryContext.inventoryTbls.Where(x => x.FlightNumber.ToLower() == tbl.FlightNumber.ToLower()).ToList();
                if (res.Count != 0)
                    throw new Exception("Inventory for airline " + tbl.AirlineNo + " is alreday exists in system");
                _inventoryContext.inventoryTbls.Add(tbl);               
                 Save();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        /// <summary>
        /// Update Inventory
        /// </summary>
        /// <param name="tbl"></param>
        public void UpdateInventory(InventoryTbl tbl)
        {
            try
            {
                _inventoryContext.Entry(tbl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Save()
        {
            try
            {
                _inventoryContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<InventoryTbl> GetAllFlightBasedUponPlaces(string fromplace, string toplace)
        {
            try
            {
                var res = _inventoryContext.inventoryTbls.Where(x => x.ToPlace.ToLower() == toplace.ToLower() && x.FromPlace.ToLower() == fromplace.ToLower()).ToList();
                if (res.Count == 0)
                    throw new Exception("No Flight exists");
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
