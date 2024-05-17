using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paris2024.Models;

[Table("OfferType")]
public class OfferType
{
    [Key]
    public int OfferTypeId { get; set; }


    [Required]
    [MaxLength(20)]
    [Display(Name = "Type d'offres")]
    public string OfferType_Name { get; set; }

    public int OfferType_AllowedPerson { get; set; }
    public virtual ICollection<Offer> Offers { get; set; } // Modif + de flexibilité (type générique)
    //public List<Offer> Offers { get; set; }
}
