namespace Paris2024.Models.Ticket
{
    public class Ticketobj
    {
        public int Id { get; set; }
        public string QrCode { get; set; }

        public int OrderDetailId { get; set; }
        public OrderItem? OrderDetail { get; set; }
    }
}
