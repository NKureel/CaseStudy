
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

        public virtual DbSet<BookflightTbl> BookflightTbls { get; set; }
        //public virtual DbSet<UserDetailTbl> UserDetailTbls { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        { 
            base.OnModelCreating(model);
            model.Entity<BookflightTbl>(entity =>
            {
                entity.ToTable("bookflightTbl");
               entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("Id");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmailId).HasMaxLength(50);

                entity.Property(e => e.FlightNumber).HasMaxLength(50);

                entity.Property(e => e.Meal).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

               // entity.Property(e => e.PersonId).HasColumnName("personId");

                entity.Property(e => e.Pnr).HasMaxLength(50);

                entity.Property(e => e.Class).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
                //entity.HasOne(d => d.Person)
                //    .WithMany(p => p.BookflightTbls)
                //    .HasForeignKey(d => d.PersonId)
                //    .HasConstraintName("FK_bookflightTbl_UserDetailTbl");
            });

            //model.Entity<UserDetailTbl>(entity =>
            //{
            //    entity.HasKey(e => e.PeopleId);

            //    entity.ToTable("UserDetailTbl");
            //   entity.Property(e => e.PeopleId).ValueGeneratedNever().HasColumnName("PeopleId");
            //    entity.Property(e => e.Age)
            //        .HasMaxLength(10)
            //        .IsFixedLength(true);

            //    entity.Property(e => e.Class).HasMaxLength(50);

            //    entity.Property(e => e.FirstName).HasMaxLength(50);

            //    entity.Property(e => e.Gender).HasMaxLength(50);

            //    entity.Property(e => e.LastName).HasMaxLength(50);
            //});
            //model.Entity<Person>(x =>
            //{
            //    x.ToTable("Person");
            //    x.Property(e => e.peopleId)
            //        .ValueGeneratedNever()
            //        .HasColumnName("PeopleId");
            //    x.Property(p => p.FirstName);
            //    x.Property(p => p.LastName);
            //    x.Property(p => p.Age);
            //    x.Property(p => p.Gender);
            //}
            //);
            //model.Entity<UserBookingTbl>().HasOne(d => d.userDetail)
                   //.WithMany(p => p.bookingdetailsofUser)
                   // .HasForeignKey(d => d.personId);                   
            //model.Entity<UserBookingTbl>().HasOne<Person>(e => e.peopleId).WithOne(d => d.User)
            //  .IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            //model.Entity<Person>().Property(e => e.PeopleId).ValueGeneratedOnAdd();
            //model.Entity<Person>().HasOne(e => e.User).WithOne(u => u.peopleId).HasPrincipalKey<Person>(e => e.PeopleId).HasForeignKey<UserBookingTbl>(e => e.peopleId);

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
