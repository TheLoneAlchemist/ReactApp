using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Models.Common
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public bool IsExpired { get; set; }
        public string? Token { get; set; }
        public bool IsRevoked { get; set; }

    }
}
