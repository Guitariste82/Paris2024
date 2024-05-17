using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Paris2024.Repositories;


public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> AddItem(int OfferId, int qty)
    {
        string userId = GetUserId();
        string cookieId = GetSessionId();

        // Switch to Cookie if user not login or use UserId
        string CombinedCardId = SetCartId(userId, cookieId);

        using var transaction = _context.Database.BeginTransaction();

        try
        {

            //if (string.IsNullOrEmpty(userId))
            //    throw new UnauthorizedAccessException("user is not logged-in");
            var cart = await GetCart(CombinedCardId);

            if (string.IsNullOrEmpty(userId))
            {
                userId = string.Empty;
            }
            if (string.IsNullOrEmpty(cookieId))
            {
                cookieId = string.Empty;
            }




            if (cart is null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Cart_CookieId = cookieId
                };

                _context.Carts.Add(cart);
            }

            _context.SaveChanges();

            // cart detail section
            var cartItem = _context.CartItems
                              .FirstOrDefault(a => a.CartId == cart.CardId && a.OfferId == OfferId);


            if (cartItem is not null)
            {
                cartItem.CartItem_Quantity += qty;
            }
            else
            {
                var offer = _context.Offers.Find(OfferId);
                cartItem = new CartItem
                {
                    OfferId = OfferId,
                    CartId = cart.CardId,
                    CartItem_Quantity = qty,
                    CartItem_UnitPrice = offer.Offer_UnitPrice  // it is a new line after update
                };
                _context.CartItems.Add(cartItem);
            }
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
        }
        var cartItemCount = await GetCartItemCount(CombinedCardId);
        return cartItemCount;
    }


    public async Task<int> RemoveItem(int offerId)
    {
        //using var transaction = _context.Database.BeginTransaction();
        string userId = GetUserId();
        string cookieId = GetSessionId();
        try
        {
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("user is not logged-in");

            var cart = await GetCart(cookieId);
            if (cart is null)
                throw new InvalidOperationException("Invalid cart");
            // cart detail section
            var cartItem = _context.CartItems
                              .FirstOrDefault(a => a.CartId == cart.CardId && a.OfferId == offerId);
            if (cartItem is null)
                throw new InvalidOperationException("Not items in cart");
            else if (cartItem.CartItem_Quantity == 1)
                _context.CartItems.Remove(cartItem);
            else
                cartItem.CartItem_Quantity = cartItem.CartItem_Quantity - 1;
            _context.SaveChanges();
        }
        catch (Exception ex)
        {

        }
        var cartItemCount = await GetCartItemCount(cookieId);
        return cartItemCount;
    }

    public async Task<Cart> GetUserCart()
    {
        var userId = GetUserId();
        var cookieId = GetSessionId();

        //if (userId == null)
        //    throw new InvalidOperationException("Invalid userid");


        var shoppingCart = await _context.Carts
                              .Include(a => a.CartItems)
                              .ThenInclude(a => a.Offer)
                              //.Include(a => a.CartItems)
                              //.ThenInclude(a => a.Offer)
                              .ThenInclude(a => a.OfferType)
                              .Where(a => a.Cart_CookieId == cookieId).FirstOrDefaultAsync();
        return shoppingCart;

    }
    public async Task<Cart> GetCart(string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(x => x.Cart_CookieId == userId);
        return cart;
    }

    public async Task<int> GetCartItemCount(string userId = "")
    {
        string CombineUserCookieID = string.Empty;

        if (!string.IsNullOrEmpty(userId)) // updated line
        {
            userId = GetUserId();
        }

        string cookieId = GetSessionId();


        CombineUserCookieID = SetCartId(userId, cookieId);


        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Cart_CookieId == CombineUserCookieID);

        return cart?.CartItems.Count ?? 0;

    }

    public async Task<bool> DoCheckout(CheckoutVM model)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            // logic
            // move data from cartDetail to order and order detail then we will remove cart detail
            var userId = GetUserId();
            var cookieId = GetSessionId();

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged-in");

            var cart = await GetCart(cookieId);
            if (cart is null)
                throw new InvalidOperationException("Invalid cart");

            var cartDetail = _context.CartItems
                                .Where(a => a.CartId == cart.CardId).ToList();
            if (cartDetail.Count == 0)
                throw new InvalidOperationException("Cart is empty");


            var order = new Order
            {
                UserId = userId,
                Order_CreateDate = DateTime.UtcNow,
                Order_Name = model.Name,
                Order_Email = model.Email,
                Order_MobileNumber = model.MobileNumber,
                Order_PaymentMethod = model.PaymentMethod,
                Order_Address = model.Address,
                Order_IsPaid = true,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var item in cartDetail)
            {
                string GeneratedKey2Guid = Guid.NewGuid().ToString();

                var orderItem = new OrderItem
                {
                    OfferId = item.OfferId,
                    OrderId = order.OrderId,
                    OrderItem_Quantity = item.CartItem_Quantity,
                    OrderItem_UnitPrice = item.CartItem_UnitPrice,
                    OrderItem_Key2 = GeneratedKey2Guid,
                    OrderItem_QrCode = userId + GeneratedKey2Guid
                };
                _context.OrderItems.Add(orderItem);

            }

            // removing the cartdetails
            _context.CartItems.RemoveRange(cartDetail);
            _context.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private string GetUserId()
    {
        var principal = _httpContextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(principal);
        return userId;
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

    private string SetCartId(string userId, string cookieId)
    {
        string CurrentCardId = string.Empty;

        if (!string.IsNullOrEmpty(cookieId))
        {
            return CurrentCardId = cookieId;
        }
        else
        {
            return string.Empty;
        }



        if (string.IsNullOrEmpty(userId) && (!string.IsNullOrEmpty(cookieId)))
        {
            return CurrentCardId = cookieId;
        }

        if (!string.IsNullOrEmpty(userId))
        {
            CurrentCardId = userId;
        }



        return CurrentCardId;
    }
}