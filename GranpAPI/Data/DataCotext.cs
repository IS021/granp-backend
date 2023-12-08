using Granp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Granp.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Professional> Professionals { get; set; }
        public DbSet<ProfessionalReview> ProfessionalReviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            // Professional
            modelBuilder.Entity<Professional>()
                .OwnsOne(
                    p => p.Address,
                    a =>
                    {
                        a.OwnsOne(a => a.Location);
                    });
                
            modelBuilder.Entity<Professional>().HasIndex(p => p.UserId).IsUnique();

            modelBuilder.Entity<Professional>()
                .OwnsOne(
                    p => p.TimeTable,
                    t =>
                    {
                        t.OwnsMany(t => t.TimeSlots); // This is a collection of value objects (TimeSlot) NEEDS TO BE SOLVED
                    });

            // Customer
            modelBuilder.Entity<Customer>()
                .OwnsOne(
                    c => c.ElderAddress,
                    a =>
                    {
                        a.OwnsOne(a => a.Location);
                    });

            modelBuilder.Entity<Customer>().HasIndex(c => c.UserId).IsUnique();
        }
    }
}