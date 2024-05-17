
using Microsoft.EntityFrameworkCore;

namespace Paris2024.Repositories;

public class AdminOfferTypeRepository : IAdminOfferTypeRepository
{
    private readonly ApplicationDbContext _context;

    public AdminOfferTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OfferType?> GetOfferTypeById(int id)
    {
        return await _context.OfferTypes.FindAsync(id);
    }

    public async Task AddOfferType(OfferType category)
    {
        _context.OfferTypes.Add(category);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateOfferType(OfferType category)
    {
        _context.OfferTypes.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOfferType(OfferType category)
    {
        _context.OfferTypes.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OfferType>> GetOfferTypes()
    {
        return await _context.OfferTypes.ToListAsync();
    }

}


