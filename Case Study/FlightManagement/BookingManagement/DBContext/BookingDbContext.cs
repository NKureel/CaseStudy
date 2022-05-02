
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace BookingManagement.DBContext
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

       public  DbSet<UserBookingTbl> bookingTbls { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        { 
            base.OnModelCreating(model);            
            model.Entity<UserBookingTbl>().HasEnum(e => e.Meal);            
        }
    }
    public static class Extensions
    {
        public static void HasEnum<TEntity, TProperty>(this EntityTypeBuilder<TEntity> entityBuilder, Expression<Func<TEntity, TProperty>> propertyExpression)
      where TEntity : class
      where TProperty : Enum
        {
            entityBuilder.Property(propertyExpression)
                .HasConversion(
                    v => v.ToString(),
                    v => (TProperty)Enum.Parse(typeof(TProperty), v)
                );
        }
    }
}
