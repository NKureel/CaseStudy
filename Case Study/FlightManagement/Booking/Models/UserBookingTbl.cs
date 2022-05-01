using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models
{
    public class UserBookingTbl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public int NoOfSeatBook { get; set; }
        public Person People { get; set; }

        public Food Meal { get; set; }
        public string SeatNo { get; set; }

        public string FlightNumber { get; set; }

        public string Pnr { get; set; }

    }

    public enum Food
    {
        Veg,
        NonVeg,
        None
    }
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public UserIdentity Gender { get; set; }

        public int age { get; set; }
    }

    public enum UserIdentity
    {
        Female,
        Male,
        Transgender
    }
}
