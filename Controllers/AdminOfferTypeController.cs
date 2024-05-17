using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Paris2024.Controllers;


    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminOfferTypeController : Controller
    {
        private readonly IAdminOfferTypeRepository _offerTypeRepo;

        public AdminOfferTypeController(IAdminOfferTypeRepository offerTypeRepo)
        {
            _offerTypeRepo = offerTypeRepo;
        }

        public async Task<IActionResult> Index()
        {
            var offerTypes = await _offerTypeRepo.GetOfferTypes();
            return View(offerTypes);
        }

        public IActionResult AddOfferType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOfferType(OfferTypeDto offerType)
        {
            if (!ModelState.IsValid)
            {
                return View(offerType);
            }
            try
            {
                var categoryToAdd = new OfferType
                {
                    OfferTypeId = offerType.OfferTypeDto_Id,
                    OfferType_Name = offerType.OfferTypeDto_Name,
                    OfferType_AllowedPerson = offerType.OfferTypeDto_AllowedPerson
                };

                await _offerTypeRepo.AddOfferType(categoryToAdd);
                TempData["successMessage"] = "Type d'offre ajouter avec succès";
                return RedirectToAction(nameof(AddOfferType));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Type d'offre non ajoutée";
                return View(offerType);
            }
        }

        public async Task<IActionResult> UpdateOfferType(int id)
        {
            var offerType = await _offerTypeRepo.GetOfferTypeById(id);
            if (offerType is null)
                throw new InvalidOperationException($"Ce type d'offre : {id} n'a pas été trouvé");

            var categoryToUpdate = new OfferTypeDto
            {
                OfferTypeDto_Id = offerType.OfferTypeId,
                OfferTypeDto_Name = offerType.OfferType_Name,
                OfferTypeDto_AllowedPerson = offerType.OfferType_AllowedPerson
            };
            return View(categoryToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOfferType(OfferTypeDto offerTypeToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(offerTypeToUpdate);
            }
            try
            {
                var offerType = new OfferType
                {
                    OfferTypeId = offerTypeToUpdate.OfferTypeDto_Id,
                    OfferType_Name = offerTypeToUpdate.OfferTypeDto_Name,
                    OfferType_AllowedPerson = offerTypeToUpdate.OfferTypeDto_AllowedPerson
                };

                await _offerTypeRepo.UpdateOfferType(offerType);
                TempData["successMessage"] = "Type d'offre mise à jour";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Type d'offre non mise à jour";
                return View(offerTypeToUpdate);
            }
        }

        public async Task<IActionResult> DeleteOfferTypeConfirmation(int id)
        {
            var offerType = await _offerTypeRepo.GetOfferTypeById(id);
            if (offerType is null)
                throw new InvalidOperationException($"Ce type d'offre : {id} n'a pas été trouvé");
            return View(offerType);

        }
        public async Task<IActionResult> DeleteOfferType(int id)
        {
            var offerType = await _offerTypeRepo.GetOfferTypeById(id);
            if (offerType is null)
                throw new InvalidOperationException($"Ce type d'offre : {id} n'a pas été trouvé");
            await _offerTypeRepo.DeleteOfferType(offerType);
            return RedirectToAction(nameof(Index));

        }

    }
