namespace Paris2024.Models.ViewModels;

public class Ticket
{
    public int Id { get; set; }
    public QRCodeModel QRCodeModel { get; set; }

    public string QrCode { get; set; }

    public int OrderDetailId { get; set; }
    public OrderItem? OrderDetail { get; set; }
}
