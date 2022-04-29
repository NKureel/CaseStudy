using AirlineManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace AirlineManagement.DBContext
{
    public class AirlineDbContext:DbContext
    {

        public AirlineDbContext(DbContextOptions<AirlineDbContext> options):base(options)
        {

        }

        
        public DbSet<AirlineTbl> airlineTbls { get; set; }
        public DbSet<InventoryTbl> inventoryTbls { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<InventoryTbl>().Property(p => p.TicketCost).HasColumnType("decimal(8,2)");
            model.Entity<InventoryTbl>().HasEnum(e => e.ScheduleDays);
            model.Entity<InventoryTbl>().HasEnum(e => e.Meal);
            model.Entity<InventoryTbl>().HasEnum(e => e.InstrumentUsed);
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
