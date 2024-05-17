using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paris2024.Models;

[Table("OrderItem")]
public class OrderItem
{
    [Required]
    [Key]
    public int OrderItemId { get; set; }

    [Required]
    public int OrderItem_Quantity { get; set; }

    [Required]
    public double OrderItem_UnitPrice { get; set; }

    [Required]
    public string OrderItem_Key2 { get; set; }
    public string OrderItem_QrCode { get; set; }

    [Required]
    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }
    public Order Order { get; set; }

    [Required]
    [ForeignKey(nameof(Offer))]
    public int OfferId { get; set; }
    public Offer Offer { get; set; }
}
