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
    [Display(Name = "Quantité")]
    public int OrderItem_Quantity { get; set; }

    [Required]
    [Display(Name = "Prix unitaire")]
    public double OrderItem_UnitPrice { get; set; }

    [Required]
    [Display(Name = "Key2")]
    public string OrderItem_Key2 { get; set; }

    [Display(Name = "QrCode")]
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
