﻿namespace Paris2024.Models.ViewModels
{
    public class OfferVM
    {
        public IEnumerable<Offer>? Offers { get; set; }
        public IEnumerable<OfferType>? OfferTypes { get; set; }
        public string SearchString { get; set; } = "";
        public int OfferTypeId { get; set; } = 0;

        public string Offer_Sport { get; set; } = "";
        public string OfferType_Name { get; set; } = "";
        public int Offer_Id { get; set; } = 0;

    }
}
