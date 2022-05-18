
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
    public class InventoryRepository : IInventoryRepository, IConsumer<BookflightTbl>
    {
        private readonly InventoryDbContext _inventoryContext;
        public InventoryRepository(InventoryDbContext context)
        {
            _inventoryContext = context;
        }
       
        public async Task Consume(ConsumeContext<BookflightTbl> context)
        {
            try
            {                
                string flightno = context.Message.FlightNumber;             
                var tbl = _inventoryContext.inventoryTbls.Find(flightno);             
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _inventoryContext.Entry(tbl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await this._inventoryContext.SaveChangesAsync();
                    scope.Complete();
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Update Flight Details
        /// </summary>
        /// <param name="flightno"></param>
        /// <param name="seatclass"></param>
        /// <param name="seatno"></param>
        //public void UpdateFlightDetail(string flightno, string seatclass, string seatno)
        //{
        //    try
        //    {
        //        var res = _inventoryContext.flightDetail.Where(x => x.FlightNumber.ToLower().Trim() == flightno.ToLower().Trim() && x.seatNo.ToLower().Trim() == seatno.ToLower().Trim() && x.SeatClass.ToLower().Trim() == seatclass.ToLower().Trim()).ToList();
        //        if (res.Count != 0)
        //        {
        //            foreach (var flight in res)
        //            {
        //                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //                {
        //                    flight.status = SeatStatus.Booked.ToString();
        //                    _inventoryContext.Entry(flight).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //                    Save();
        //                    scope.Complete();
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
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
                 // AddFlightDetail(tbl.FlightNumber, tbl.BusinessClassSeat.ToString(), tbl.NonBusinessClassSeat.ToString());
                 Save();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public void AddFlightDetail(string flightno, string businessClass, string NonBusinessclass)
        //{
        //    var totalseat = Convert.ToInt64(businessClass) + Convert.ToInt64(NonBusinessclass);
        //    for (int i = 0; i < Convert.ToInt32(totalseat); i++)
        //    {
        //        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //        {
        //            FlightBookingDetails flight = new FlightBookingDetails();
        //            flight.FlightNumber = flightno;
        //            flight.seatNo = "A" + i.ToString();
        //            if (i <= Convert.ToInt32(businessClass))
        //                flight.SeatClass = Seatclass.Business.ToString();
        //            else
        //                flight.SeatClass = Seatclass.NonBusiness.ToString();
        //            flight.status = SeatStatus.NotBooked.ToString();
        //            _inventoryContext.flightDetail.Add(flight);
        //            Save();
        //            scope.Complete();

        //        }
        //    }
       // }
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
