using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Paris2024.Dtos.Offer;

public class OfferDto
{
    public int OfferDto_OfferId { get; set; }

    [Required]
    [MaxLength(255)]
    public string? OfferDto_OfferCode { get; set; }

    [Required]
    [MaxLength(50)]
    public string? OfferDto_Sport { get; set; }
    [Required]
    [MaxLength(255)]
    public string? OfferDto_Description { get; set; }
    public string? OfferDto_ImagePath { get; set; }

    [Required]
    public double OfferDto_UnitPrice { get; set; }

    public DateTime OfferDto_EventDate { get; set; }

    [Required]
    public int OfferDto_OfferTypeId { get; set; }
    public IFormFile? OfferDto_ImageFile { get; set; }
    public IEnumerable<SelectListItem>? OfferTypeList { get; set; }
}


