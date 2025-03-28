using System;
namespace AuthorizationService.Domain.Models
{
    public class OTP
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public bool IsUsed { get; set; }

        // Внешний ключ и навигационные свойства
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}

