using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class UserFavoriteAd
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; } = null!;
        public int AdID { get; set; }
        public Ad Ad { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
