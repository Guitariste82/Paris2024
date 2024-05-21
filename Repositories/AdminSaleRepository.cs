using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Paris2024.Repositories;

public class AdminSaleRepository : IAdminSaleRepository
{
    private readonly ApplicationDbContext _context;
    public AdminSaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SalesPerOfferVM>> GetSalesPerOffer()
    {
        return await _context.OrderItems
            .Include(oi => oi.Offer)
            .ThenInclude(o => o.OfferType)
            .GroupBy(oi => new {
                oi.Offer.OfferId,
                oi.Offer.Offer_Sport,
                oi.Offer.Offer_Code, 
                oi.Offer.Offer_Description, 
                oi.Offer.OfferType.OfferType_Name })
            .Select(g => new SalesPerOfferVM
            {
                Offer_Designation = g.Key.Offer_Description,
                Offer_Sport = g.Key.Offer_Sport,
                Offer_Code = g.Key.Offer_Code,
                OfferType_Name = g.Key.OfferType_Name,
                SalesCount = g.Sum(oi => oi.OrderItem_Quantity)
            })
            .ToListAsync();
    }

}




