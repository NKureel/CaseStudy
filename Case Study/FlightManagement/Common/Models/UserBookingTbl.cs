using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class UserBookingTbl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public int? NoOfSeatBook { get; set; }

        public int? personId { get; set; }

                      
        public  Person[] userDetail { get; set; }
            

        public string Meal { get; set; }
        public string SeatNo { get; set; }
        public string SeatClass { get; set; }

        public string FlightNumber { get; set; }     
        public  string Pnr {  get; set; }
       
    }
    public class FlightBookingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    public class Person
    {
        //public Person()
        //{
        //    bookingdetailsofUser = new HashSet<UserBookingTbl>();
        //}             
        //public Person(string firstname,string lastname, string gender,int age)
        //{
        //    FirstName = firstname;
        //    LastName = lastname;
        //    Gender = gender;
        //    Age = age;
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int peopleId { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string LastName { get; set; }

        public int? Age { get; set; }
        public Seatclass Class { get; set; }
        //public virtual ICollection<UserBookingTbl> bookingdetailsofUser { get; set; }
        //public virtual UserBookingTbl User { get; set; }
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
