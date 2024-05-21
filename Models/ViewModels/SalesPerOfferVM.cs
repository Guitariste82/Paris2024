namespace Paris2024.Models.ViewModels
{
    public class SalesPerOfferVM
    {
        public int OfferId { get; set; }
        public string Offer_Code { get; set; }
        public string Offer_Sport { get; set; }
        public string OfferType_Name { get; set; }
        
        public string Offer_Designation { get; set; }
        public int SalesCount { get; set; }

        public OfferType OfferType { get; set; }
    }
}
