using Microsoft.AspNetCore.Mvc;

namespace Paris2024.Interfaces;

public interface IAdminSaleRepository
{
    Task<List<SalesPerOfferVM>> GetSalesPerOffer();
}
