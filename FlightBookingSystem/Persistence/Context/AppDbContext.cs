// Backend.Persistence.Context.AppDbContext.cs
using Backend.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users", "flight_booking_schema")
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Flight>().ToTable("flights", "flight_booking_schema");
            modelBuilder.Entity<Booking>().ToTable("bookings", "flight_booking_schema");
            modelBuilder.Entity<Aircraft>().ToTable("aircraft", "flight_booking_schema");
            modelBuilder.Entity<Company>().ToTable("companies", "flight_booking_schema");
            modelBuilder.Entity<Airport>().ToTable("airports", "flight_booking_schema");
            modelBuilder.Entity<CompanyRegistrationRequest>().ToTable("company_registration_requests", "flight_booking_schema");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Flight)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FlightId);

            modelBuilder.Entity<Aircraft>()
                .HasOne(a => a.Company)
                .WithMany(c => c.Aircrafts)
                .HasForeignKey(a => a.CompanyId);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Aircraft)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AircraftId);
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.OriginAirport)
                .WithMany(a => a.OriginFlights)
                .HasForeignKey(f => f.OriginAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DestinationAirport)
                .WithMany(a => a.DestinationFlights)
                .HasForeignKey(f => f.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Company>()
                .HasOne(c => c.CompanyRegistrationRequest)
                .WithOne(cr => cr.Company)
                .HasForeignKey<CompanyRegistrationRequest>(cr => cr.CompanyId);

            modelBuilder.Entity<CompanyRegistrationRequest>()
                .HasOne(cr => cr.User)
                .WithOne(u => u.CompanyRegistrationRequest)
                .HasForeignKey<CompanyRegistrationRequest>(cr => cr.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<CompanyRegistrationRequest> CompanyRegistrationRequests { get; set; }
    }
}
