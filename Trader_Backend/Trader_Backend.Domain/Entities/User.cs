using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string FullName { get; set; } = null!;
        public string EntraID { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PersonalPictureURL { get; set; }
        public string Role { get; set; } = "User";
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public ICollection<InvitationCode> CreatedInvitationCodes { get; set; } = new List<InvitationCode>();
        public ICollection<UserFavoriteAd> FavoriteAds { get; set; } = new List<UserFavoriteAd>();
        public ICollection<Ad> Ads { get; set; } = new List<Ad>();

    }
}
