
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
        public DbSet<Person> person { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        { 
            base.OnModelCreating(model);            
            model.Entity<UserBookingTbl>().HasEnum(e => e.Meal);
            model.Entity<Person>().HasEnum(e => e.Gender);            
            model.Entity<UserBookingTbl>().HasOne(p => p.peopleId);
            model.Entity<Person>(x => { x.ToTable("Person");x.HasKey(k => k.PeopleId);
                x.Property(p => p.Name);
                x.Property(p => p.Age);
                x.Property(p => p.Gender);
            }
            ) ;
            model.Entity<UserBookingTbl>().HasOne<Person>(e => e.peopleId).WithOne(d => d.User)
              .IsRequired(true).OnDelete(DeleteBehavior.Cascade);

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
