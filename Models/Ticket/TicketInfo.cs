using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Paris2024.Models.Ticket
{
    public class TicketInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string? Offer_Code { get; set; }
        public string? Offer_Sport { get; set; }
        public string? Offer_Description { get; set; }

        public double Offer_UnitPrice { get; set; }

        public string? OfferType_Name { get; set; }
        //public int OfferTypeId { get; set; }
        //public OfferType OfferType { get; set; }

    }
}
