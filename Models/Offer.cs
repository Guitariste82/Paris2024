using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paris2024.Models;

[Table("Offer")]
public class Offer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? Offer_Code { get; set; }

    [Required]
    [StringLength(100)]
    public string? Offer_Sport { get; set; }

    [Required]
    [StringLength(255)]
    public string? Offer_Description { get; set; }

    public string? Offer_ImagePath { get; set; }

    [Required]
    public double Offer_UnitPrice { get; set; }

    [Required]
    [ForeignKey(nameof(OfferType))]
    public int OfferTypeId { get; set; }
    public OfferType OfferType { get; set; }

}
