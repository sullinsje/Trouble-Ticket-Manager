using Trouble_Ticket_Manager.Models.Entities;
// using Trouble_Ticket_Manager.Services;

namespace Trouble_Ticket_Manager.Services;

public class Initializer
{
    private readonly ApplicationDbContext _db;

    public Initializer(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task SeedDatabaseAsync()
    {
        await _db.Database.EnsureCreatedAsync();

        if (_db.Tickets.Any())
        {
            return; // Database already seeded
        }

        // Seed Contacts
        var contact1 = new Contact { Name = "Alice Smith", Email = "alice.s@company.com", Building = "A", Room = "101" };
        var contact2 = new Contact { Name = "Bob Jones", Email = "bob.j@company.com", Building = "B", Room = "202" };

        await _db.Contacts.AddRangeAsync(contact1, contact2);
        await _db.SaveChangesAsync();

        // Seed Computers
        var compA = new Computer
        {
            AssetTag = "000000",
            ServiceTag = "SV12345",
            Model = "Dell XPS 13",
            Contact = contact1,
            UnderWarranty = MockWarrantyChecker.GetMockWarranty("SV12345")
        };
        var compB = new Computer
        {
            AssetTag = "000001",
            ServiceTag = "SV67890",
            Model = "HP ProBook",
            Contact = contact2,
            UnderWarranty = MockWarrantyChecker.GetMockWarranty("SV67890")
        };

        await _db.Computers.AddRangeAsync(compA, compB);
        await _db.SaveChangesAsync();

        // Seed Tickets
        var ticket1 = new Ticket
        {
            ContactId = contact1.Id, 
            SubmittedAt = DateTime.UtcNow.AddHours(-48),
            IsResolved = false,
            ChargerGiven = false
        };

        var ticket2 = new Ticket
        {
            ContactId = contact2.Id,
            SubmittedAt = DateTime.UtcNow.AddHours(-24),
            IsResolved = false,
            ChargerGiven = true
        };

        var ticket3 = new Ticket
        {
            ContactId = contact1.Id,
            SubmittedAt = DateTime.UtcNow.AddHours(-12),
            IsResolved = true,
            ChargerGiven = false
        };


        // Seed the Many-to-Many Join Table (TicketComputers) ---
        // Ticket 1 involves Computer A
        ticket1.TicketComputers.Add(new TicketComputer
        {
            Computer = compA,
            IssueDescription = "No boot after BIOS update."
        });

        // Ticket 2 involves Computer B
        ticket2.TicketComputers.Add(new TicketComputer
        {
            Computer = compB,
            IssueDescription = "Broken USB-C port."
        });

        // Ticket 3 involves Computer A (Computer A can be on multiple tickets)
        ticket3.TicketComputers.Add(new TicketComputer
        {
            Computer = compA,
            IssueDescription = "Screen flickers intermittently."
        });

        // Save all changes
        await _db.Tickets.AddRangeAsync(ticket1, ticket2, ticket3);
        await _db.SaveChangesAsync();
    }
}

