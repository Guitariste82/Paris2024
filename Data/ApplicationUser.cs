using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Paris2024.Data;

/// <summary>
 /// Inherit from Identity
 /// </summary>
public class ApplicationUser : IdentityUser
{
    //FH - add two extented field in dbo.AspNetUsers

    [Required]
    [StringLength(40, ErrorMessage = "Le prénom ne peut pas dépasser 40 caractères.")]
    [Display(Name = "Prénom")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(40, ErrorMessage = "Le nom ne peut pas dépasser 40 caractères.")]
    [Display(Name = "Nom")]
    public string? LastName { get; set; }
}
