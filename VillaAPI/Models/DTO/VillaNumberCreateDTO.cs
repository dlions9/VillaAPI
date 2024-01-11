using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.DTO
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        [Required]
        public int VNumber { get; set; }
    }
}
