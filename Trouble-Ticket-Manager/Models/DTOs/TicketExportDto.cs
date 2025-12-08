using CsvHelper.Configuration.Attributes;

namespace Trouble_Ticket_Manager.Models.DTOs
{
    public class TicketExportDto
    {
        [Name("Ticket ID")]
        public int Id { get; set; }

        [Name("Submitted On")]
        public string SubmittedAtDisplay { get; set; } = string.Empty;

        [Name("Contact Name")]
        public string ContactName { get; set; } = string.Empty;

        [Name("Contact Email")]
        public string ContactEmail { get; set; } = string.Empty;

        [Name("Asset Tags")]
        public string ComputerAssetTags { get; set; } = string.Empty;
        [Name("Models")]
        public string ComputerModels { get; set; } = string.Empty;
        [Name("Issues")]
        public string IssueDescription { get; set; } = string.Empty;

        [Name("Resolved")]
        public string IsResolved { get; set; } = string.Empty;

        [Name("Charger Given")]
        public string ChargerGiven { get; set; } = string.Empty;
    }
}