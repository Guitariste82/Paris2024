using Microsoft.EntityFrameworkCore;

namespace Paris2024.Repositories;



public class AdminOfferRepository : IAdminOfferRepository
{
    private readonly ApplicationDbContext _context;
    public AdminOfferRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Offer?> GetOfferById(int id) => await _context.Offers.FindAsync(id);

    public async Task<IEnumerable<Offer>> GetOfferks() => await _context.Offers.Include(a => a.OfferType).ToListAsync();

    public async Task AddOffer(Offer offer)
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOffer(Offer offer)
        {
            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOffer(Offer offer)
        {
            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();
        }

  
}