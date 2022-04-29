using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineManagement.Models
{
    public class InventoryTbl
    {
        [Key]
        public string FlightNumber { get; set; }

        [ForeignKey("AirlineNo")]
        public virtual AirlineTbl Airlines { get; set; }
        [ForeignKey("AirlineNo")]

        [Display(Name ="Airline")]
        public virtual string AirlineNo { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Days ScheduleDays { get; set; }
        public Instrument InstrumentUsed { get; set; }
        public int BusinessClassSeat { get; set; }
        public int NonBusinessClassSeat { get; set; }
        [Column(TypeName ="decimal(8,2)")]
        public decimal TicketCost { get; set; }
        public int NoOfRows { get; set; }
        public Food Meal { get; set; }

    }

    public enum Food
    {
        None,
        Veg,
        NonVeg
    }
    public enum Days
    {
        Daily,
        WeekDays,
        Weekend,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public enum Instrument
    {
        A320,
        neo,
        A450,
        A789,
        C67h

    }
}
