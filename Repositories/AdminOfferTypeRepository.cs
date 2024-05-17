
namespace Paris2024.Repositories;

public class AdminOfferTypeRepository : IAdminOfferTypeRepository
{
    Task IAdminOfferTypeRepository.AddOfferType(OfferType category)
    {
        throw new NotImplementedException();
    }

    Task IAdminOfferTypeRepository.DeleteOfferType(OfferType category)
    {
        throw new NotImplementedException();
    }

    Task<OfferType?> IAdminOfferTypeRepository.GetOfferTypeById(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<OfferType>> IAdminOfferTypeRepository.GetOfferTypes()
    {
        throw new NotImplementedException();
    }

    Task IAdminOfferTypeRepository.UpdateOfferType(OfferType category)
    {
        throw new NotImplementedException();
    }
}
