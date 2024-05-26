using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paris2024.Models;

namespace Paris2024.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TicketApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public TicketApiController(ApplicationDbContext context,  UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test api Ok");
    }

    [HttpGet("GetTicket")]
    public async Task <IActionResult> GetTicket([FromQuery] string qrcodeKey)
    {
      
            if (qrcodeKey == null)
            {
                throw new Exception("Ticket non valide");
            }

        string id = qrcodeKey.Substring(0, 36);
        string key2 = qrcodeKey.Substring(36);


        // FH - Init Field
        string RealUserFirstName=string.Empty;
        string RealUserLastName = string.Empty;
        string RealUserPhone = string.Empty;
        string RealUserName = string.Empty;
        string RealUserEmail = string.Empty;


        bool userExists = await IsUserExists(id);
        if (!userExists)
        {
            return BadRequest($"Ticket non valide , utilisateur inconnu pour ce QrCode: {qrcodeKey}");
        }
        else
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            RealUserFirstName = user.FirstName;
            RealUserLastName = user.LastName;
            RealUserPhone=user.PhoneNumber;
            RealUserName = user.UserName;
            RealUserEmail = user.Email;
        }

        //string UserName = User.Identity.Name;
        string UserName = RealUserName;

        var OrderDetail =  _context.OrderItems
                  .Include(s => s.Offer)
                  .ThenInclude(ot => ot.OfferType)
                  .FirstOrDefaultAsync(m => m.OrderItem_Key2 == key2);

            if (OrderDetail.Result == null)
            {
            return BadRequest($"Ticket non valide (clé sécurité erronée) : {key2}"); // ToDo chercher diff with NotFound() ou StatusCode() 
            }

        var ticketInfo = new TicketInfo
        {
            FirstName = RealUserFirstName,
            LastName = RealUserLastName,
            Phone = RealUserPhone,
            UserName = RealUserName,
            Email = RealUserEmail,
            Offer_Code= OrderDetail.Result.Offer.Offer_Code,
            Offer_Sport = OrderDetail.Result.Offer.Offer_Sport,
            Offer_Description = OrderDetail.Result.Offer.Offer_Description,
            Offer_UnitPrice = OrderDetail.Result.Offer.Offer_UnitPrice,
            OfferType_Name = OrderDetail.Result.Offer.OfferType.OfferType_Name
        };

        return Ok(new { message = $"Utilisateur vérifié : {ticketInfo.UserName} pour Ticket avec QRCode: {qrcodeKey}", user = ticketInfo });
    }
    //For Test
    //https://localhost:7210/api/TicketApi/GetTicket?qrcodeKey=c6476c45-cf43-4eaa-bc7d-a1245033a90b171b976d-a2a8-4e2b-a65e-4b0766263b90
    //https://localhost:7210/api/TicketApi/GetTicket?qrcodeKey=c6476c45-cf43-4eaa-bc7d-a1245033a90b304d27fd-de05-43e4-9107-5083f288da14


    [HttpPost]
    public IActionResult Post([FromBody] MyModel model)
    {
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

        public async Task<bool> IsUserExists(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null;
    }


}
