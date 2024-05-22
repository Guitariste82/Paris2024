using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Paris2024.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminSaleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAdminSaleRepository _adminSaleRepository;

        public AdminSaleController(ApplicationDbContext context, IAdminSaleRepository adminSaleRepository)
        {
            _context = context;
            _adminSaleRepository = adminSaleRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> getSalesPerOffer()
        {
            var salesPerOfferVM = await _adminSaleRepository.GetSalesPerOffer();
            return View(salesPerOfferVM);
        }


    }
}
