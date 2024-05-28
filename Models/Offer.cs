using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paris2024.Models;

[Table("Offer")]
public class Offer
{
    [Key]
    public int OfferId { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Code")]
    public string? Offer_Code { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Sport")]
    public string? Offer_Sport { get; set; }

    [Required]
    [StringLength(255)]
    [Display(Name = "Description")]
    public string? Offer_Description { get; set; }

    public string? Offer_ImagePath { get; set; }

    [Required]
    [Display(Name = "Prix")]
    public double Offer_UnitPrice { get; set; }

       [Display(Name = "Date")]
    public DateTime Offer_EventDate { get; set; }

    [Required]
    [ForeignKey(nameof(OfferType))]
    public int OfferTypeId { get; set; }
    public OfferType OfferType { get; set; }

}
