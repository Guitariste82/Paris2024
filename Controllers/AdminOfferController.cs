using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Paris2024.Data;
using Paris2024.Models;
using Stripe;

namespace Paris2024.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class AdminOfferController : Controller
{
    private readonly IAdminOfferRepository _offerRepo;
    private readonly IAdminOfferTypeRepository _categoryRepo;
    private readonly IFileService _fileService;

    public AdminOfferController(IAdminOfferRepository offerRepo, IAdminOfferTypeRepository categoryRepo, IFileService fileService)
    {
        _offerRepo = offerRepo;
        _categoryRepo = categoryRepo;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index()
    {
        var offers = await _offerRepo.GetOfferks();
        return View(offers);
    }

    public async Task<IActionResult> AddOffer()
    {
        var categorySelectList = (await _categoryRepo.GetOfferTypes()).Select(category => new SelectListItem
        {
            Text = category.OfferType_Name,
            Value = category.OfferTypeId.ToString(),
        });

        OfferDto offerToAdd = new()
        {
            OfferTypeList = categorySelectList
        };
        return View(offerToAdd);
    }

    [HttpPost]
    public async Task<IActionResult> AddOffer(OfferDto offerToAdd)
    {
        var categorySelectList = (await _categoryRepo.GetOfferTypes()).Select(category => new SelectListItem
        {
            Text = category.OfferType_Name,
            Value = category.OfferTypeId.ToString(),
        });

        offerToAdd.OfferTypeList = categorySelectList;

        if (!ModelState.IsValid)
            return View(offerToAdd);

        try
        {
            if (offerToAdd.OfferDto_ImageFile != null)
            {
                if (offerToAdd.OfferDto_ImageFile.Length > 1 * 512 * 512)
                {
                    throw new InvalidOperationException("La taille du logo ne peut pas exceder 512 ko");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                string imageName = await _fileService.SaveFile(offerToAdd.OfferDto_ImageFile, allowedExtensions);
                offerToAdd.OfferDto_ImagePath = imageName;
            }
            // manual mapping of OfferDTO -> Offer
            Offer offer = new()
            {
                OfferId = offerToAdd.OfferDto_Id,
                Offer_Code = offerToAdd.OfferDto_OfferCode,
                Offer_Sport = offerToAdd.OfferDto_Sport,
                Offer_Description = offerToAdd.OfferDto_Description,
                Offer_ImagePath = offerToAdd.OfferDto_ImagePath,
                OfferTypeId = offerToAdd.OfferDto_OfferTypeId,
                Offer_UnitPrice = offerToAdd.OfferDto_UnitPrice
            };
            await _offerRepo.AddOffer(offer);
            TempData["successMessage"] = "Offre ajoutée avec succès";
            return RedirectToAction(nameof(AddOffer));
        }
        //catch (InvalidOperationException ex)
        //{
        //    TempData["errorMessage"]= ex.Message;
        //    return View(offerToAdd);
        //}
        //catch (FileNotFoundException ex)
        //{
        //    TempData["errorMessage"] = ex.Message;
        //    return View(offerToAdd);
        //}
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Erreur durant sauvegarde offre" + ex.Message;
            return View(offerToAdd);
        }
    }

    public async Task<IActionResult> UpdateOffer(int id)
    {
        var offer = await _offerRepo.GetOfferById(id);
        if (offer == null)
        {
            TempData["errorMessage"] = $"L'offre avec l'id: {id} non trouvé";
            return RedirectToAction(nameof(Index));
        }
        var categorySelectList = (await _categoryRepo.GetOfferTypes()).Select(category => new SelectListItem
        {
            Text = category.OfferType_Name,
            Value = category.OfferTypeId.ToString(),
            Selected = category.OfferTypeId == offer.OfferId
        });
        OfferDto offerToUpdate = new()
        {
            OfferTypeList = categorySelectList,
            OfferDto_OfferCode = offer.Offer_Code,
            OfferDto_Sport = offer.Offer_Sport,
            OfferDto_Description = offer.Offer_Description,
            OfferDto_OfferTypeId = offer.OfferId,
            OfferDto_UnitPrice = offer.Offer_UnitPrice,
            OfferDto_ImagePath = offer.Offer_ImagePath
        };
        return View(offerToUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateOffer(OfferDto offerToUpdate)
    {
        var categorySelectList = (await _categoryRepo.GetOfferTypes()).Select(category => new SelectListItem
        {
            Text = category.OfferType_Name,
            Value = category.OfferTypeId.ToString(),
            Selected = category.OfferTypeId == offerToUpdate.OfferDto_OfferTypeId
        });
        offerToUpdate.OfferTypeList = categorySelectList;

        if (!ModelState.IsValid)
            return View(offerToUpdate);

        try
        {
            string oldImage = "";
            if (offerToUpdate.OfferDto_ImageFile != null)
            {
                if (offerToUpdate.OfferDto_ImageFile.Length > 1 * 512 * 512)
                {
                    throw new InvalidOperationException("La taille du logo ne peut pas exceder 512 ko");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];

                string imageName = await _fileService.SaveFile(offerToUpdate.OfferDto_ImageFile, allowedExtensions);
                oldImage = offerToUpdate.OfferDto_ImagePath;
                offerToUpdate.OfferDto_ImagePath = imageName;
            }
            // manual mapping of offerDTO -> Offer
            Offer offer = new()
            {
                OfferId = offerToUpdate.OfferDto_Id,
                Offer_Code = offerToUpdate.OfferDto_OfferCode,
                Offer_Sport = offerToUpdate.OfferDto_Sport,
                Offer_Description = offerToUpdate.OfferDto_Description,
                OfferTypeId = offerToUpdate.OfferDto_OfferTypeId,
                Offer_UnitPrice = offerToUpdate.OfferDto_UnitPrice,
                Offer_ImagePath = offerToUpdate.OfferDto_ImagePath
            };

            await _offerRepo.UpdateOffer(offer);
            // if image is updated, then delete it from the folder too
            if (!string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
            TempData["successMessage"] = "Offre mise à jour avec succès";
            return RedirectToAction(nameof(Index));
        }
  
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Erreur durant sauvegarde offre" + ex.Message;
            return View(offerToUpdate);
        }
    }

    public async Task<IActionResult> DeleteOffer(int id)
    {
        try
        {
            var offer = await _offerRepo.GetOfferById(id);
            if (offer == null)
            {
                TempData["errorMessage"] = $"L'offre avec l'id: {id} non trouvé";
            }
            else
            {
                await _offerRepo.DeleteOffer(offer);
                if (!string.IsNullOrWhiteSpace(offer.Offer_ImagePath))
                {
                    _fileService.DeleteFile(offer.Offer_ImagePath);
                }
            }
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Erreur durant l'effacement de l'offre " + ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }
}
