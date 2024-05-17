using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace Paris2024.Controllers;


[Authorize]
public class UserOrderController : Controller
{
    private readonly IUserOrderRepository _userOrderRepo;
    private readonly IQrCodeGeneratorRepository _qrCodeGeneratorRepo;

    public UserOrderController(IUserOrderRepository userOrderRepo, IQrCodeGeneratorRepository qrCodeGeneratorRepo)
    {
        _userOrderRepo = userOrderRepo;
        _qrCodeGeneratorRepo = qrCodeGeneratorRepo;

    }
    public async Task<IActionResult> UserOrders()
    {
        var orders = await _userOrderRepo.UserOrders();
        return View(orders);
    }

    public async Task<IActionResult> GetTicket(string? TicketID)
    {
        QRCodeModel model = new QRCodeModel();
        var viewModel1 = await _userOrderRepo.TicketsDetails(TicketID);

        if (viewModel1 == null)
        {
            return NotFound();
        }

        string SecureKey = viewModel1.OrderItem_QrCode;
        string url = "https://localhost:7158/UserOrder/GetTicket?TicketID=";

        string QrCodeImage = _qrCodeGeneratorRepo.GetQrCodeToPngWithUrl(url, SecureKey);

        model.QRImageURL = "data:image/png;base64," + QrCodeImage;

        //https://localhost:7158/UserOrder/GetTicket?TicketID=ad7142e9-da94-4275-b975-0eade52091d75e562cd2-7dae-4e4b-8b22-e84455ee42b5

        var viewModel = new Ticket
        {
            OrderDetail = viewModel1,
            QRCodeModel = model
        };

        return View(viewModel);

    }
}
