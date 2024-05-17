namespace Paris2024.Interfaces
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetOffers(int offerTypeId = 0, string searchString = "");
        Task<IEnumerable<OfferType>> OfferTypes();
    }
}
