using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{

    public class BookflightTbl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Meal { get; set; }
        public string FlightNumber { get; set; }
        public string Pnr { get; set; }
        public int? peopleid { get; set; }        

    }
    //public class FlightBookingDetails
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    //    public int id { get; set; }

    //    public string FlightNumber { get; set; }
    //    public string seatNo { get; set; }
    //    public string SeatClass { get; set; }        

    //    public string status { get; set; }
    //}
    // [Owned]   
    public enum SeatStatus
    {
        Booked,
        NotBooked
    }

    public class TicketDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Emailid { get; set; }
        public string Meal { get; set; }
        public string FlightNumber { get; set; }
        public string Pnr { get; set; }
        public DateTime? startDateTime = null;
        public DateTime StartDateTime
        {
            get
            {
                return this.startDateTime.HasValue
                   ? this.startDateTime.Value
                   : DateTime.Now;
            }

            set { this.startDateTime = value; }
        }
        public DateTime? endDateTime = null;
        public DateTime EndDateTime
        {
            get
            {
                return this.endDateTime.HasValue
                   ? this.endDateTime.Value
                   : DateTime.Now;
            }

            set { this.endDateTime = value; }
        }
        public string ScheduleDays { get; set; }
    }
    public class UserDetailTbl
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PeopleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Class { get; set; }

        
    }

    public class BookflightTblUsr
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Meal { get; set; }
        public string FlightNumber { get; set; }
        public string Pnr { get; set; }
        public int count { get; set; }
        public UserDetailTbl[] users { get; set; }
    }
    public enum Seatclass
    {
        Business,
        NonBusiness
    }
    public enum UserIdentity
    {
        Female,
        Male,
        Transgender
    }
}
