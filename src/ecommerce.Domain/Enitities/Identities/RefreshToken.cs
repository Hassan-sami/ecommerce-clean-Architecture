using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ecommerce.Domain.common;

namespace ecommerce.Domain.Enitities.Identities;

public class RefreshToken  :BaseEntity
{
    
    public string AppUserId { get; set; }
    public string? Token { get; set; }
    public string? RefreshTokenString { get; set; }
    public string? JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime AddedTime { get; set; }
    public DateTime ExpiryDate { get; set; }
    [ForeignKey(nameof(AppUserId))]
    public virtual AppUser? AppUser { get; set; }
}