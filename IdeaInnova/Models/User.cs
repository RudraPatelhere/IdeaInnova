using System.ComponentModel.DataAnnotations;  //provides attributes for data validation

namespace IdeaInnova.Models
{
    //repreesnts a user of the IdeaInnova system
    public class User
    {
        [Key]          //marks as the primary key in database
        public int Id { get; set; }

        [Required]           //ensures that Username field is not empty
        public string Username { get; set; }

        [Required]          //ensures that Password field is not empty
        public string Password { get; set; }

        //Represents the user's role in the system, defaulting to "user" if no role is specified.
        public string Role { get; set; } = "User";
    }
}