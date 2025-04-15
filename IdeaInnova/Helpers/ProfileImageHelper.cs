using System.Collections.Generic;
using System.Net;

namespace IdeaInnova.Helpers
{
    public static class ProfileImageHelper
    {
        // Dictionary that maps exact usernames to specific image file paths.
        // This is used as an exact match fallback if desired.
        private static readonly Dictionary<string, string> KnownUserImages = new Dictionary<string, string>
        {
            { "Jiya Pandit", "/images/jiya.jpg" },
            { "Komalpreet Kaur", "/images/komal.jpg" },
            { "Rudra Patel", "/images/rudra.jpg" },
            { "Drasti Patel", "/images/drasti.jpg" }
        };

        //Returns the profile image URL for the given username.
        //If the username contains certain key phrases, it returns a custom image path; otherwise, it falls back to random avatar.
        public static string GetProfileImageUrl(string username)
        {
            //Check if username is not null or empty.
            if (!string.IsNullOrEmpty(username))
            {
                //Convert the username to lowercase to perform case-insensitive checks.
                string lowerUsername = username.ToLower();

                // Check for partial match for "drasti"
                if (lowerUsername.Contains("drasti"))
                {
                    return "/images/drasti.jpg";
                }

                // Check for partial match for "jiya"
                if (lowerUsername.Contains("jiya"))
                {
                    return "/images/jiya.jpg";
                }

                //check for partial match for 'komal'
                if (lowerUsername.Contains("komalpreet") || lowerUsername.Contains("komal"))
                {
                    return "/images/komal.jpg";
                }

                //check for partial match for "rudra"
                if (lowerUsername.Contains("rudra"))
                {
                    return "/images/rudra.jpg";
                }
            }

            // Fallback to a random avatar if no match found.
            string encodedName = WebUtility.UrlEncode(username ?? "User");
            return $"https://ui-avatars.com/api/?name={encodedName}&background=random";
        }
    }
}
