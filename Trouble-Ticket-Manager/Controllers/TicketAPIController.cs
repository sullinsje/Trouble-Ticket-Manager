using Microsoft.AspNetCore.Mvc;
using Trouble_Ticket_Manager.Services;
using Trouble_Ticket_Manager.Models.ViewModels;
using Trouble_Ticket_Manager.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Trouble_Ticket_Manager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketAPIController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly IComputerRepository _computerRepo;
        private readonly IContactRepository _contactRepo;

        public TicketAPIController(ITicketRepository ticketRepo, IComputerRepository computerRepo, IContactRepository contactRepo)
        {
            _ticketRepo = ticketRepo;
            _computerRepo = computerRepo;
            _contactRepo = contactRepo;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] TicketViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.NewContactName))
            {
                ModelState.Remove(nameof(model.SelectedContactId));
            }
            else 
            {
                ModelState.Remove(nameof(model.NewContactName));
                ModelState.Remove(nameof(model.NewContactEmail));
                ModelState.Remove(nameof(model.NewContactBuilding));
                ModelState.Remove(nameof(model.NewContactRoom));
            }
            
            if (model.SelectedAssetTags != null && model.SelectedAssetTags.Any() && string.IsNullOrWhiteSpace(model.NewAssetTag))
            {
                ModelState.Remove(nameof(model.NewAssetTag));
                ModelState.Remove(nameof(model.NewComputerServiceTag));
                ModelState.Remove(nameof(model.NewComputerModel));
            }
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            int finalContactId;

            if (!string.IsNullOrWhiteSpace(model.NewContactName))
            {
                try
                {
                    var newContact = new Contact
                    {
                        Name = model.NewContactName,
                        Email = model.NewContactEmail!,
                        Building = model.NewContactBuilding,
                        Room = model.NewContactRoom
                    };

                    var createdContact = await _contactRepo.CreateAsync(newContact);
                    finalContactId = createdContact.Id;
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to create new contact: {ex.Message}");
                }
            }
            else
            {
                if (model.SelectedContactId.HasValue && model.SelectedContactId.Value > 0)
                {
                    finalContactId = model.SelectedContactId.Value;
                }
                else
                {
                    return BadRequest("Contact selection is required.");
                }
            }

         
            var finalAssetTags = new List<string>();

            if (!string.IsNullOrWhiteSpace(model.NewAssetTag))
            {
                if (await _computerRepo.ReadAsync(model.NewAssetTag) != null)
                {
                    return BadRequest($"Computer with Asset Tag '{model.NewAssetTag}' already exists.");
                }

                try
                {
                    var newComputer = new Computer
                    {
                        AssetTag = model.NewAssetTag,
                        ServiceTag = model.NewComputerServiceTag!,
                        Model = model.NewComputerModel!,
                        ContactId = finalContactId
                    };

                    await _computerRepo.CreateAsync(newComputer);
                    finalAssetTags.Add(model.NewAssetTag);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to create new computer: {ex.Message}");
                }
            }

            
            if (model.SelectedAssetTags != null)
            {
                finalAssetTags.AddRange(model.SelectedAssetTags.Where(tag => !string.IsNullOrWhiteSpace(tag)));
            }

            if (!finalAssetTags.Any())
            {
                return BadRequest("At least one computer (new or existing) must be associated with the ticket.");
            }


            var ticketComputers = finalAssetTags.Select(tag => new TicketComputer
            {
                IssueDescription = model.IssueDescription,
                ComputerAssetTag = tag,
            }).ToList();


            var ticketEntity = new Ticket
            {
                ContactId = finalContactId,
                SubmittedAt = model.SubmittedAt,
                IsResolved = model.IsResolved,
                ChargerGiven = model.ChargerGiven,
                TicketComputers = ticketComputers
            };

            var ticketResult = await _ticketRepo.CreateAsync(ticketEntity);

            if (ticketResult != null && ticketResult.Id > 0)
            {
                return Ok(new { ticketId = ticketResult.Id, message = "Ticket created successfully." });
            }

            return BadRequest("Failed to create ticket.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketRepo.ReadAllAsync();
            return Ok(tickets);
        }

        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var ticket = await _ticketRepo.ReadAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ticketRepo.DeleteAsync(id);
            return NoContent(); 
        }


        [HttpPost("Close/{id}")] 
        public async Task<IActionResult> Close(int id)
        {
            var originalTicket = await _ticketRepo.ReadAsync(id);
            if (originalTicket == null)
            {
                return NotFound($"Ticket ID {id} not found.");
            }

            originalTicket.IsResolved = true;

            await _ticketRepo.UpdateAsync(id, originalTicket);

            return Ok(new { message = $"Ticket {id} successfully closed." });
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] TicketViewModel model)
        {
            var originalTicket = await _ticketRepo.ReadAsync(id);
            if (originalTicket == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(model.NewContactName))
            {
                ModelState.Remove(nameof(model.SelectedContactId));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int finalContactId;

            if (!string.IsNullOrWhiteSpace(model.NewContactName))
            {
                try
                {
                    var newContact = new Contact
                    {
                        Name = model.NewContactName,
                        Email = model.NewContactEmail!,
                        Building = model.NewContactBuilding,
                        Room = model.NewContactRoom
                    };
                    var createdContact = await _contactRepo.CreateAsync(newContact);
                    finalContactId = createdContact.Id;
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to create new contact: {ex.Message}");
                }
            }
            else if (model.SelectedContactId.HasValue && model.SelectedContactId.Value > 0)
            {
                finalContactId = model.SelectedContactId.Value;
            }
            else
            {
                finalContactId = originalTicket.ContactId;
            }

            var finalAssetTags = new List<string>();

            if (!string.IsNullOrWhiteSpace(model.NewAssetTag))
            {
                if (await _computerRepo.ReadAsync(model.NewAssetTag) != null)
                {
                    return BadRequest($"Computer with Asset Tag '{model.NewAssetTag}' already exists.");
                }

                try
                {
                    var newComputer = new Computer
                    {
                        AssetTag = model.NewAssetTag,
                        ServiceTag = model.NewComputerServiceTag!,
                        Model = model.NewComputerModel!,
                        ContactId = finalContactId
                    };
                    await _computerRepo.CreateAsync(newComputer);
                    finalAssetTags.Add(model.NewAssetTag);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to create new computer: {ex.Message}");
                }
            }

            if (model.SelectedAssetTags != null)
            {
                finalAssetTags.AddRange(model.SelectedAssetTags.Where(tag => !string.IsNullOrWhiteSpace(tag)));
            }
            
            if (!finalAssetTags.Any())
            {
                if (originalTicket.TicketComputers.Any())
                {
                    finalAssetTags.AddRange(originalTicket.TicketComputers.Select(tc => tc.ComputerAssetTag));
                }
                else
                {
                    return BadRequest("At least one computer must be associated with the ticket.");
                }
            }



            originalTicket.ContactId = finalContactId;

            if (model.SubmittedAt != default(DateTime) && model.SubmittedAt != DateTime.MinValue)
            {
                originalTicket.SubmittedAt = model.SubmittedAt;
            }

            originalTicket.IsResolved = model.IsResolved;
            originalTicket.ChargerGiven = model.ChargerGiven;

            originalTicket.TicketComputers.Clear(); 
            
            foreach (var tag in finalAssetTags.Distinct()) 
            {
                originalTicket.TicketComputers.Add(new TicketComputer
                {
                    ComputerAssetTag = tag,
                    IssueDescription = model.IssueDescription, 
                    TicketId = originalTicket.Id 
                });
            }


            await _ticketRepo.UpdateAsync(id, originalTicket);

            return Ok(new { ticketId = originalTicket.Id, message = "Ticket updated successfully." });
        }
    }
}