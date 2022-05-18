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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Class { get; set; }

        // public virtual UserDetailTbl Person { get; set; }

    }
    public class FlightBookingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        
        public string FlightNumber { get; set; }
        public string seatNo { get; set; }
        public string SeatClass { get; set; }

        public string status { get; set; }
    }
    // [Owned]   
    public enum SeatStatus
    {
        Booked,
        NotBooked
    }

    
    //public class UserDetailTbl
    //{
    //    //public Person()
    //    //{
    //    //    bookingdetailsofUser = new HashSet<UserBookingTbl>();
    //    //}
    //    //public Person(string firstname, string lastname, string gender, int age)
    //    //{
    //    //    FirstName = firstname;
    //    //    LastName = lastname;
    //    //    Gender = gender;
    //    //    Age = age;
    //    //}
    //    //public UserDetailTbl()
    //    //{
    //    //    BookflightTbls = new HashSet<BookflightTbl>();
    //    //}
        
    //    public int? PeopleId { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Gender { get; set; }
    //    public string Age { get; set; }
    //    public string Class { get; set; }

    //    //public virtual ICollection<BookflightTbl> BookflightTbls { get; set; }
    //    //public virtual ICollection<UserBookingTbl> bookingdetailsofUser { get; set; }
    //    //public virtual UserBookingTbl User { get; set; }
    //}

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
