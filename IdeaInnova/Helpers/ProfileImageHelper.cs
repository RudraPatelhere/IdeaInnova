using System.Collections.Generic;
using System.Net;

namespace IdeaInnova.Helpers
{
    public static class ProfileImageHelper
    {
        private static readonly Dictionary<string, string> KnownUserImages = new Dictionary<string, string>
        {
            { "Jiya Pandit", "/images/jiya.jpg" },
            { "Komalpreet Kaur", "/images/komal.jpg" },
            { "Rudra Patel", "/images/rudra.jpg" },
            { "Drasti Patel", "/images/drasti.jpg" }
        };

        public static string GetProfileImageUrl(string username)
        {
            if (!string.IsNullOrEmpty(username) && KnownUserImages.ContainsKey(username))
            {
                return KnownUserImages[username];
            }
            // Fallback to a random or generic avatar
            string encodedName = WebUtility.UrlEncode(username ?? "User");
            return $"https://ui-avatars.com/api/?name={encodedName}&background=random";
        }
    }
}