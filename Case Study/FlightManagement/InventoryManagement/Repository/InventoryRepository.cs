
using Common.Models;
using InventoryManagement.DBContext;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Repository
{
    public class InventoryRepository : IInventoryRepository, IConsumer<UserBookingTbl>
    {
        private readonly InventoryDbContext _inventoryContext;
        public InventoryRepository(InventoryDbContext context)
        {
            _inventoryContext = context;
        }

        public Task Consume(ConsumeContext<UserBookingTbl> context)
        {            
            string flightno = context.Message.FlightNumber;
            Seatclass seatclass = context.Message.SeatClass;
            string seatno = context.Message.SeatNo;
            var tbl = _inventoryContext.inventoryTbls.Find(flightno);
            tbl.NonBusinessClassSeat -= 1;         
            _inventoryContext.Entry(tbl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            UpdateFlightDetail(flightno, seatclass, seatno);
            Save();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Update Flight Details
        /// </summary>
        /// <param name="flightno"></param>
        /// <param name="seatclass"></param>
        /// <param name="seatno"></param>
        public void UpdateFlightDetail(string flightno, Seatclass seatclass, string seatno)
        {
            try
            {
                FlightBookingDetails flight = new FlightBookingDetails();
                flight.FlightNumber = flightno;
                flight.SeatClass = seatclass;
                flight.seatNo = seatno;
                flight.status = SeatStatus.Booked;
                _inventoryContext.Entry(flight).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Save();
            }
            catch { }
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
                _inventoryContext.inventoryTbls.Add(tbl);
                AddFlightDetail(tbl.FlightNumber, tbl.BusinessClassSeat.ToString(), tbl.NonBusinessClassSeat.ToString());
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddFlightDetail(string flightno,string businessClass,string NonBusinessclass)
        {
            var totalseat = businessClass + NonBusinessclass;
            for (int i = 0; i < Convert.ToInt32(totalseat); i++)
            {
                FlightBookingDetails flight = new FlightBookingDetails();
                flight.FlightNumber = flightno;                 
                    flight.seatNo = "A" + i.ToString();
                if (i != Convert.ToInt32(businessClass))
                    flight.SeatClass = Seatclass.NonBusiness;
                else
                    flight.SeatClass = Seatclass.Business;
                flight.status = SeatStatus.NotBooked;
                _inventoryContext.flightdetails.Add(flight);
                Save();
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
                var res= _inventoryContext.inventoryTbls.Where(x => x.ToPlace.ToLower() == toplace.ToLower() && x.FromPlace.ToLower() == fromplace.ToLower()).ToList();
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
