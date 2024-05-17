using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paris2024.Models;

[Table("Order")]
public class Order
{
    [Key]
    public int OrderId { get; set; }
    
    [Required]
    public DateTime Order_CreateDate { get; set; } = DateTime.UtcNow;

    [Required]
    public int Order_StatusId { get; set; }

    [Required]
    [MaxLength(30)]
    public string? Order_Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string? Order_Email { get; set; }

    [Required]
    public string? Order_MobileNumber { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Order_Address { get; set; }

    [Required]
    [MaxLength(30)]
    public string? Order_PaymentMethod { get; set; }

    public bool Order_IsPaid { get; set; }

    public string UserId { get; set; }
    public List<OrderItem> OrderItem { get; set; }

}
