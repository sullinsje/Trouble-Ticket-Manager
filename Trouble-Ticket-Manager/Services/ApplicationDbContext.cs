using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComputer> TicketComputers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuration for Ticket -> User (Contact)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Contact)
                .WithMany()
                .HasForeignKey(t => t.ContactId)
                .OnDelete(DeleteBehavior.Restrict); // <-- Prevents deleting a User if they have related Tickets

            // Configuration for Computer -> User
            modelBuilder.Entity<Computer>()
                .HasOne(c => c.Contact)
                .WithMany(u=> u.Computers)
                .HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Restrict); // <-- Prevents deleting a User if they have related Computers
        }
    }
}