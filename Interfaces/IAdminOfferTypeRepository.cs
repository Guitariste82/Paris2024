namespace Paris2024.Interfaces
{
    public interface IAdminOfferTypeRepository
    {
        Task<OfferType?> GetOfferTypeById(int id);

        Task AddOfferType(OfferType category);
        Task UpdateOfferType(OfferType category);
        Task DeleteOfferType(OfferType category);
        Task<IEnumerable<OfferType>> GetOfferTypes();
    }
}
