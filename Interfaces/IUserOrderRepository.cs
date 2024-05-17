namespace Paris2024.Interfaces;

public interface IUserOrderRepository
{
    Task<Order?> GetOrderById(int id);
    Task<IEnumerable<Order>> UserOrders(bool getAll = false);

    Task<OrderItem> TicketsDetails(string? TicketID);
}
