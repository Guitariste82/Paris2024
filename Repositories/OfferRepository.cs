using Microsoft.EntityFrameworkCore;
using Paris2024.Models;

namespace Paris2024.Repositories;


public class OfferRepository : IOfferRepository
{
    private readonly ApplicationDbContext _context;
    public OfferRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OfferType>> OfferTypes()
    {
        return await _context.OfferTypes.ToListAsync();
    }

    public async Task<IEnumerable<Offer>> GetOffers(int offerTypeId = 0, string searchString = "")
    {
        searchString = searchString.ToLower();

        var offersQuery = _context.Offers
            .Include(offer => offer.OfferType)
            .Select(offer => new Offer
            {
                OfferId = offer.OfferId,
                Offer_Code = offer.Offer_Code,
                Offer_Sport = offer.Offer_Sport,
                Offer_Description = offer.Offer_Description,
                Offer_ImagePath = offer.Offer_ImagePath,
                Offer_UnitPrice = offer.Offer_UnitPrice,
                OfferTypeId = offer.OfferTypeId,
                //Quantity = 50
            });

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            offersQuery = offersQuery.Where(offer => offer.Offer_Sport.ToLower().StartsWith(searchString));
        }

        if (offerTypeId > 0)
        {
            offersQuery = offersQuery.Where(offer => offer.OfferTypeId == offerTypeId);
        }

        return await offersQuery.ToListAsync();
    }

    public async Task<Offer> GetOfferById(int offerId = 0)
    {
        var offerQuery = _context.Offers
        .Include(offer => offer.OfferType)
        .FirstOrDefaultAsync(offer => offer.OfferId == offerId);

        return await offerQuery;
    }

  



}