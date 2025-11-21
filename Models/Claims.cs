using System.ComponentModel.DataAnnotations;

namespace PROGPOEst10439216.Models
{
    public class Claims
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public string? Status { get; set; }
        public string? Documentation { get; set; }
        public double? Hours { get; set; }
        public string? Lecturer { get; set; }
        public decimal? Rate { get; set; }

        public string? Notes { get; set; }

        public string? FileName { get; set; }
        public string? FilePath { get; set; }
    }
}
