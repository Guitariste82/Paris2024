using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Paris2024.Repositories;


public class UserOrderRepository : IUserOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;


    public UserOrderRepository(ApplicationDbContext context,UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<Order?> GetOrderById(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
    {
        var orders = _context.Orders
                       .Include(x => x.OrderItem)
                       .ThenInclude(x => x.Offer)
                       .ThenInclude(x => x.OfferType).AsQueryable();
        if (!getAll)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            orders = orders.Where(a => a.UserId == userId);
            return await orders.ToListAsync();
        }

        return await orders.ToListAsync();
    }

    private string GetUserId()
    {
        var principal = _httpContextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(principal);
        return userId;
    }


    public async Task<OrderItem> TicketsDetails(string? qrcodeKey)
    {
        if (qrcodeKey == null)
        {
            throw new Exception("User is not logged-in");
        }

        var OrderDetail = await _context.OrderItems
              .Include(s => s.Offer)
              .FirstOrDefaultAsync(m => m.OrderItem_QrCode == qrcodeKey);

        if (OrderDetail == null)
        {
            throw new Exception("User is not logged-in");
        }

        return OrderDetail;



    }




}
