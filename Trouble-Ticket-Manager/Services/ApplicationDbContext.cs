using Microsoft.EntityFrameworkCore;
using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComputer> TicketComputers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration for Ticket -> User (Contact)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Contact)
                .WithMany()
                .HasForeignKey(t => t.ContactId)
                .OnDelete(DeleteBehavior.Restrict); // <-- Prevents deleting a User if they have related Tickets

            // Configuration for Computer -> User
            modelBuilder.Entity<Computer>()
                .HasOne(c => c.User)
                .WithMany(u=> u.Computers)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // <-- Prevents deleting a User if they have related Computers
        }
    }
}