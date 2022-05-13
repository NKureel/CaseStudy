using AirlineManagement.DBContext;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
/*
 Created By: Naina Kureel
 Detail: Airline Management Repository
*/
namespace AirlineManagement.Repository
{
    public class AirlineRepository : IAirlineRepository
    {
        private readonly AirlineDbContext _airlineDb;
        public AirlineRepository(AirlineDbContext context)
        {
            _airlineDb = context;
        }
        
        /// <summary>
        /// Block Airline
        /// </summary>
        /// <param name="airlineNo"></param>
        public void DeleteAirline(string airlineNo)
        {            
            try
            {
                var airline = _airlineDb.airlineTbls.Find(airlineNo);
                if (airline != null)
                {
                   _airlineDb.airlineTbls.Remove(airline);
                    Save();
                    return;
                }
                throw new System.Exception("Failed to delete the airline");
                
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Airline by Airline no
        /// </summary>
        /// <param name="airlineNo"></param>
        /// <returns></returns>
        public AirlineTbl GetAirlineByNumber(string airlineNo)
        {
            AirlineTbl res = new AirlineTbl();
            try
            {
                 res = _airlineDb.airlineTbls.Find(airlineNo);
                if (res != null)
                    return res;               
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
            return res;
        }


        /// <summary>
        /// Get list of airlines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AirlineTbl> GetAirlines()
        {
            List<AirlineTbl> res = new List<AirlineTbl>();
            try
            {
                res= _airlineDb.airlineTbls.ToList();
                if (res.Count == 0)
                    throw new Exception("No Airlines exists");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

       
        /// <summary>
        /// Insert new airlines
        /// </summary>
        /// <param name="tbl"></param>
        public void InsertAirline(AirlineTbl tbl)
        {
            try
            {
                _airlineDb.airlineTbls.Add(tbl);
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Save changes
        /// </summary>
        public void Save()
        {
            try
            {
                _airlineDb.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Db Update Failed " + ex.Message);
            }
            
        }

        /// <summary>
        /// Update Airline detail by airline
        /// </summary>
        /// <param name="tbl"></param>
        public void UpdateAirline(AirlineTbl tbl)
        {
            try
            {
                _airlineDb.Entry(tbl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }       
       
    }
}
