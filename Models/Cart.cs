using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paris2024.Models;

[Table("Cart")]
public class Cart
{
    [Key]
    public int CardId { get; set; }

    public string? UserId { get; set; }

    public string? Cart_CookieId { get; set; }

    public bool Cart_IsDeleted { get; set; } = false;

    public ICollection<CartItem>? CartItems { get; set; } // Fh - Add Generic Type
}
