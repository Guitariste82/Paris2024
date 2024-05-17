using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paris2024.Models;

[Table("CartItem")]
public class CartItem
{
    [Key]
    public int CartItemId { get; set; }

    [Required]
    public int CartItem_Quantity { get; set; }

    [Required]
    public double CartItem_UnitPrice { get; set; }

    [Required]
    [ForeignKey(nameof(Offer))]
    public int OfferId { get; set; }
    [Required]
    public Offer? Offer { get; set; }

    [Required]
    [ForeignKey(nameof(Cart))]
    public int CartId { get; set; }
    public Cart? Cart { get; set; }
}
