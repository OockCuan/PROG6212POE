using System.ComponentModel.DataAnnotations;

namespace PROGPOEst10439216.Models
{
    public class Profiles
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Department { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal DefaultRatePerJob { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
