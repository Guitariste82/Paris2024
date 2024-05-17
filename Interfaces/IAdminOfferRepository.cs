namespace Paris2024.Interfaces;

public interface IAdminOfferRepository
{
    Task<Offer?> GetOfferById(int id);
    Task AddOffer(Offer offer);
    Task DeleteOffer(Offer offer);
    
    Task<IEnumerable<Offer>> GetOfferks();
    Task UpdateOffer(Offer offer);
}
