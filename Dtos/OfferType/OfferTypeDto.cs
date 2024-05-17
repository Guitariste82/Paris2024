using System.ComponentModel.DataAnnotations;

namespace Paris2024.Dtos.OfferType
{
    public class OfferTypeDto
    {
        public int OfferTypeDto_Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? OfferTypeDto_Name { get; set; }

        public int OfferTypeDto_AllowedPerson { get; set; }
    }
}
