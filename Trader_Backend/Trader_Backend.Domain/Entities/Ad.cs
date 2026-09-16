using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class Ad
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Location { get; set; } = null!;
        public string ContactPhoneNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string MainImageURL { get; set; } = null!;

        public int UserID { get; set; }
        public User User { get; set; } = null!;
        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;

        public AdData AdData { get; set; } = null!;
        public ICollection<AdImage> AdImages { get; set; } = new List<AdImage>();
        public ICollection<UserFavoriteAd> FavoritedByUsers { get; set; } = new List<UserFavoriteAd>();
    }
}
