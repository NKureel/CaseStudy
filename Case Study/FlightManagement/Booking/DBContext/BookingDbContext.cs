using Booking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.DBContext
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        DbSet<UserBookingTbl> bookingTbls { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        { base.OnModelCreating(model); }
    }
}
