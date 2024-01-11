using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaAPI.Models
{
    public class VillaNumberModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int VillaNo { get; set; }
        [ForeignKey("Villa")]
        public int VNumber { get; set; }
        public VillaModel Villa { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
