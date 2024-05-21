using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Paris2024.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TicketApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public TicketApiController(ApplicationDbContext context,  UserManager<IdentityUser> userManager)
    {
        _context = context;
       // _httpContextAccessor = httpContextAccessor;
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


        bool userExists = await IsUserExists(id);
        if (!userExists)
        {
            return BadRequest($"Ticket non valide , utilisateur inconnu pour ce QrCode: {qrcodeKey}");
        }
        else
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        string toto = User.Identity.Name;

        var OrderDetail =  _context.OrderItems
                  .Include(s => s.Offer)
                  .FirstOrDefaultAsync(m => m.OrderItem_Key2 == key2);

            if (OrderDetail.Result == null)
            {
            return BadRequest($"Ticket non valide (clé sécurité erronée) : {key2}"); // ToDo chercher diff with NotFound() ou StatusCode() 
            }

            //return OrderDetail;
            return Ok($"Utilisateur vérifié : {toto} pour Ticket avec QRCode: {qrcodeKey} ");
    }
    //https://localhost:7210/api/MyApi/GetTicket?qrcodeKey=c6476c45-cf43-4eaa-bc7d-a1245033a90b171b976d-a2a8-4e2b-a65e-4b0766263b90

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
