using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System.Diagnostics;

namespace Paris2024.Controllers;

public class PaymentController : Controller
{
    private readonly StripeSettings _stripeSettings;
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public PaymentController(
         IOptions<StripeSettings> stripeSettings,
         ApplicationDbContext context,
         IHttpContextAccessor httpContextAccessor,
         UserManager<ApplicationUser> userManager)
    {
        _stripeSettings = stripeSettings.Value;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager; // Assignation du paramètre UserManager à la variable locale
    }

    public IActionResult Checkout()
    {
        var utilisateur = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
        if (utilisateur == null)
        {
            return new LocalRedirectResult("/Identity/Account/Login");
        }

        ViewBag.email = utilisateur.Email;
        ViewBag.MobileNumber = utilisateur.PhoneNumber;

        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task<Cart> GetCart(string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(x => x.Cart_CookieId == userId);
        return cart;
    }

    public async Task<IActionResult> CreateCheckoutSessionAsync(decimal amount)
    {

        var userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User is not logged-in");


        var cookiePanierID = GetSessionId();

        var cart = await GetCart(cookiePanierID);

        if (cart is null)
            throw new InvalidOperationException("Invalid cart");

        var cartDetail = _context.CartItems
                          .Where(a => a.CartId == cart.CardId).ToList();

        if (cartDetail.Count == 0)
            throw new InvalidOperationException("Cart is empty");

        var products = _context.CartItems.Where(a => a.CartId == cart.CardId)
             .Include(s => s.Offer)
             .ToList();


        var utilisateur = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
        if (utilisateur == null)
        {
            //return RedirectToAction("Login", "AccountController");
            return new LocalRedirectResult("/Identity/Account/Login");
        }

        var lineItems = new List<SessionLineItemOptions>();
        products.ForEach(product => lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = (decimal?)(product.Offer.Offer_UnitPrice * 100),
                Currency = "eur",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "Billet jeux olympics",
                    Description = product.Offer.Offer_Description
                    //Images = new List<string> { product.ImageUrl }
                }
            },
            Quantity = product.CartItem_Quantity
        }));

        // Fh - Switch if in production or localhost
        var baseUrl = Environment.GetEnvironmentVariable("BASE_URL") ?? "https://localhost:7210";
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
    {
        "card"
    },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "https://localhost:7210/Payment/success",
            CancelUrl = "https://localhost:7210/Payment/cancel",
            //SuccessUrl = "https://indus82.com/Payment/success",
            //CancelUrl = "https://indus82.com/Payment/cancel",
            CustomerEmail = utilisateur.Email,
            //SuccessUrl = $"{baseUrl}/Payment/success",
            //CancelUrl = $"{baseUrl}/Payment/cancel",
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        TempData["StripeStatus"] = session.Id;

        Response.Headers.Add("Location", session.Url);

        return new StatusCodeResult(303);
        //return Redirect(session.Url);
    }

    public IActionResult cancel()
    {
        return View("Cancel");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private string GetUserId()
    {
        var principal = _httpContextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(principal);
        return userId;
    }
    public async Task<IActionResult> Success()
    {

        var service = new SessionService();
        Session session = service.Get(TempData["StripeStatus"].ToString());

        if (session.PaymentStatus == "paid")
        {

            var transaction = session.PaymentIntentId.ToString();

            CheckoutVM model = new CheckoutVM();
            model.Name = "success";
            model.Address = "15151";
            model.Name = "ffgfg";
            model.Email = "fabrice.g@gmail";
            model.PaymentMethod = "Stripe";
            model.MobileNumber = "336747474";
            return RedirectToAction("CheckoutPayment", "Cart", model);
        }
        else
        {
            return View("Cancel");
        }


    }
    public IActionResult Cancel()
    {
        return View();
    }

    private string GetSessionId()
    {
        var cookiePanierID = _httpContextAccessor.HttpContext.Request.Cookies["CookiePanierID"];
        if (string.IsNullOrEmpty(cookiePanierID))
        {
            cookiePanierID = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(15),
                HttpOnly = true,
                Secure = true
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("CookiePanierID", cookiePanierID, cookieOptions);
        }

        return cookiePanierID;
    }



}
