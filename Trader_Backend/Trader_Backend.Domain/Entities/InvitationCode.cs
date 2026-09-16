using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class InvitationCode
    {
        public int ID { get; set; }
        public string CodeHash { get; set; } = null!;
        public bool IsUsed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UsedAt { get; set; }
        public int CreatedByAdminID { get; set; }
        public User CreatedByAdmin { get; set; } = null!;
    }
}
