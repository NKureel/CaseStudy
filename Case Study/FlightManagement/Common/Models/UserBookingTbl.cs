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
        public int NoOfSeatBook { get; set; }

        [ForeignKey("PeopleId")]        
        public virtual Person peopleId { get; set; }
            

        public Food Meal { get; set; }
        public string SeatNo { get; set; }

        public string FlightNumber { get; set; }     
        public  string Pnr {  get; set; }
       
    }
  
   // [Owned]   
    public class Person
    {
        public Person()
        { }        
        public Person(string name, UserIdentity gender,int age)
        {
            Name = name;
            Gender = gender;
            Age = age;
        }
        [Key,ForeignKey("UserBookingTbl")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PeopleId { get; set; }
        public string Name { get; set; }
        public UserIdentity Gender { get; set; }

        public int Age { get; set; }
        public virtual UserBookingTbl User { get; set; }
    }

    public enum UserIdentity
    {
        Female,
        Male,
        Transgender
    }
}
