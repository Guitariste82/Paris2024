using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Paris2024.Data;
using Paris2024.Models;

namespace Paris2024.Controllers;

//[Authorize(Roles = nameof(Roles.Admin))]
public class OfferController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOfferRepository _offerRepository;
    public OfferController(ILogger<HomeController> logger, IOfferRepository offerRepository)
    {
        _logger = logger;
        _offerRepository = offerRepository;
    }

    public async Task<IActionResult> Index(string searchString = "", int offerTypeId = 0)
    {
        IEnumerable<Offer> offers = await _offerRepository.GetOffers(offerTypeId, searchString);
        IEnumerable<OfferType> offerTypes = await _offerRepository.OfferTypes();
        OfferVM offerModel = new OfferVM
        {
            Offers = offers,
            OfferTypes = offerTypes,
            SearchString = searchString,
            OfferTypeId = offerTypeId
        };
        return View(offerModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
