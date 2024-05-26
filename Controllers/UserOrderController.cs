using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using static System.Net.WebRequestMethods;

namespace Paris2024.Controllers;


[Authorize]
//[Authorize(Roles = nameof(Roles.Admin))]
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

    public async Task<IActionResult> GetTicket(string? qrcodeKey)
    {
        QRCodeModel model = new QRCodeModel();
        var TicketVM = await _userOrderRepo.TicketsDetails(qrcodeKey);

        if (TicketVM == null)
        {
            return NotFound();
        }

        string SecureKey = TicketVM.OrderItem_QrCode;
        //string url = "https://localhost:7158/UserOrder/GetTicket?qrcodeKey=";
        //string url = "http://api.indus82.com/UserOrder/GetTicket?qrcodeKey=";
        string url = "http://indus82.com/api/TicketApi/GetTicket?qrcodeKey=";
        //string url = "https://localhost:7210/api/TicketApi/GetTicket?qrcodeKey=";


        string QrCodeImage = _qrCodeGeneratorRepo.GetQrCodeToPngWithUrl(url, SecureKey);
        model.QRImageURL = "data:image/png;base64," + QrCodeImage;


        var viewModel = new Ticket
        {
            OrderDetail = TicketVM,
            QRCodeModel = model
        };

        return View(viewModel);

    }
}
